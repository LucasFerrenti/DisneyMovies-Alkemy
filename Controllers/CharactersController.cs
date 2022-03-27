using Alkemy.Models;
using Alkemy.Models.DTO;
using Alkemy.Models.Request;
using Alkemy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("user")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharactersRepository _characterRepository;

        public CharactersController(ICharactersRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpGet]
        public IActionResult GetView([FromQuery] CharactersQuery request)
        {
            try
            {
                var view = _characterRepository.FindQuerry(request).Select(character => new CharacterViewDTO
                {
                    Id = character.Id,
                    Image = character.Image,
                    Name = character.Name
                }).ToList();
                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetDetails(long Id)
        {
            try
            {
                var character = _characterRepository.FindByIdWithMovies(Id);
                if(character == null)
                {
                    return StatusCode(40, "Personaje Invalido");
                }
                var detail = new CharacterDTO
                {
                    Id = character.Id,
                    Name = character.Name,
                    Image = character.Image,
                    Age = character.Age,
                    Weight = character.Weigth,
                    History = character.History,
                    Movies = character.Participations.Select(parts => new MovieViewDTO
                    {
                        Id = parts.Movie.Id,
                        CreationDate = parts.Movie.CreationDate,
                        Image = parts.Movie.Image,
                        Title = parts.Movie.Title,
                    }).ToList()
                };
                return Ok(detail);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCharacterRequest resquest)
        {
            try
            {
                var character = new Character
                {
                    Name = resquest.Name,
                    Age = resquest.Age,
                    History = resquest.Historty,
                    Image = resquest.Image,
                    Weigth = resquest.Weigth,
                    Participations = resquest.MoviesId.Select(mov => new Participation
                    {
                        MovieId = mov
                    }).ToList()
                };
                _characterRepository.Save(character);
                return StatusCode(201, "Personaje Activado");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]CharacterDTO characterDTO)
        {
            try
            {
                var character =_characterRepository.FindById(characterDTO.Id);
                //check if exist in db
                if (character == null)
                    return StatusCode(400, "Id Invalido");

                //update data
                character.Age = characterDTO.Age;
                character.History = characterDTO.History;
                character.Id = characterDTO.Id;
                character.Image = characterDTO.Image;
                character.Name = characterDTO.Name;
                character.Weigth = characterDTO.Weight;
                character.Participations =
                    characterDTO.Movies.Where(mov => mov.Id > 0).Select(mov => new Participation
                    {
                        CharacterId = characterDTO.Id,
                        MovieId = mov.Id
                    }).ToList();

                //save data
                _characterRepository.Save(character);
                return StatusCode(201, "Succes");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(long Id)
        {
            try
            {
                var personaje = _characterRepository.FindById(Id);
                if (personaje == null)
                {
                    return StatusCode(401, "Id Invalido");
                }
                _characterRepository.DeleteCharacter(personaje);
                return Ok("Succes");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
