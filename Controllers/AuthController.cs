using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alkemy.Services;
using Alkemy.Models.Request;
using Alkemy.Repository;
using Alkemy.Utilities.Encrypt;
using Alkemy.Utilities.ConfirmEmail;
using System.Security.Claims;
using Alkemy.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Alkemy.Models;
using System.Web;
using Microsoft.AspNetCore.Cors;

namespace Alkemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(
            IUsersRepository userRepository,
            IAuthService authService,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] AuthRequest authRequest)
        {
            try
            {
                //Check email in use
                var user = _userRepository.FindByEmail(authRequest.Email);
                if (user != null)
                {
                    return StatusCode(400, "Correo en uso");
                }
                //Save user
                _userRepository.Save(new User
                {
                    Email = authRequest.Email,
                    Password = Encrypt.GetSHA256(authRequest.Password),
                });
                //Create token
                var token = _authService.CreateToken(new TokenData
                {
                    Claims = new Claim[]
                    {
                        new Claim("confirm", authRequest.Email)
                    },
                    Time = new TimeSpan(1, 0, 0)
                });
                //Send confirm email
                _emailService.SendAsync(new EmailContent
                {
                    EmailReceptor = authRequest.Email,
                    DisplayName = "DisneyMovies",
                    Subject = "Confirm email",
                    Body = ConfirmEmail.CreateBody(token, authRequest.Email, Request.Host.Value),
                    IsBodyHtml = true
                });

                return Ok("registro completo");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthRequest authRequest)
        {
            try
            {
                //Checks
                  //User
                var user = _userRepository.FindByEmail(authRequest.Email);
                if (user == null)
                {
                    return StatusCode(400, "Email Invalido");
                }
                  //Password
                var encryptPassword = Encrypt.GetSHA256(authRequest.Password);
                if (user.Password != encryptPassword)
                {
                    return StatusCode(400, "Contraceña Invalida");
                }
                  //Is active
                if (!user.IsActive)
                {
                    return StatusCode(500, "Usuario Desactivado");
                }
                //Create token
                var tokenData = new TokenData
                {
                    Claims = new Claim[]
                    {
                        new Claim("user", user.Email)
                    },
                    Time = new TimeSpan(0, 10, 0)
                };
                var token = _authService.CreateToken(tokenData);
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Confirm")]
        [Authorize("active")]
        public IActionResult Confirm()
        {
            try
            {
                var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "confirm");
                if (token == null)
                {
                    return StatusCode(400, "Token invalido");
                }
                var user = _userRepository.FindByEmail(token.Value);
                if(user == null)
                {
                    return StatusCode(400, "Email Invalido");
                }
                if (user.IsActive)
                {
                    return StatusCode(200, "El usuario ya esta activo");
                }
                user.IsActive = true;
                _userRepository.Save(user);
                return Ok("Usuario Activado");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Resend/{email}")]
        public IActionResult Resend(string email)
        {
            try
            {
                var user = _userRepository.FindByEmail(email);
                if(user == null)
                {
                    return StatusCode(400, "Email invalido");
                }
                if (user.IsActive)
                {
                    return StatusCode(200, "Usuario Activo");
                }
                //Create token
                var token = _authService.CreateToken(new TokenData
                {
                    Claims = new Claim[]
                    {
                        new Claim("confirm", email),
                    },
                    Time = new TimeSpan(1, 0, 0)
                });
                //Send confirm email
                _emailService.SendAsync(new EmailContent
                {
                    EmailReceptor = email,
                    DisplayName = "DisneyMovies",
                    Subject = "Confirm email",
                    Body = ConfirmEmail.CreateBody(token, email, Request.Host.Value),
                    IsBodyHtml = true
                });
                return Ok("Succes");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
