using Alkemy.Models.Request;
using Alkemy.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Alkemy.Repository;
using Alkemy.Utilities.Encrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Alkemy.Models;

namespace Alkemy.Services
{
    public class JwtAuthService : IAuthService
    {
        private readonly string _JwtKey;

        public JwtAuthService(IOptions<AppSettings> appSettings)
        {
            _JwtKey = appSettings.Value.JwtKey;
        }
        public string CreateToken(TokenData data)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var JwtKey = Encoding.ASCII.GetBytes(_JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(data.Claims),
                Expires = DateTime.UtcNow.Add(data.Time),
                SigningCredentials = new(new SymmetricSecurityKey(JwtKey),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
