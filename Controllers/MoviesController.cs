using Alkemy.Models;
using Alkemy.Models.DTO;
using Alkemy.Models.Request;
using Alkemy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Alkemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("user")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        [HttpGet]
        public IActionResult GetView([FromQuery] MoviesQuery request)
        {
            try
            {
                var view = _moviesRepository.FindQuery(request).Select(m => new MovieViewDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    CreationDate = m.CreationDate,
                    Image = m.Image
                });
                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetDetail(long id)
        {
            try
            {
                var movie = _moviesRepository.FindByIdWithCharacters(id);
                if (movie == null)
                {
                    return StatusCode(400, "Id Invalido");
                }
                var detail = new MovieDTO
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Image = movie.Image,
                    CreationDate = movie.CreationDate,
                    Qualification = movie.Qualification,
                    Genre = new GenreDTO
                    {
                        Id = movie.Genre.Id,
                        Name = movie.Genre.Name,
                        Image = movie.Genre.Image
                    },
                    Characters = movie.Participations.Select(part => new CharacterViewDTO
                    {
                        Id = part.Character.Id,
                        Name = part.Character.Name,
                        Image = part.Character.Image
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
        public IActionResult Post([FromBody] AddMovieResquest resquest)
        {
            try
            {
                var movie = new Movie
                {
                    Title = resquest.Title,
                    Image = resquest.Image,
                    CreationDate = resquest.CreationDate,
                    Qualification = resquest.Qualification,
                    GenreId = resquest.GenreId,
                    Participations = resquest.CharactersId.Select(part => new Participation
                    {
                        CharacterId = part
                    }).ToList()
                };
                _moviesRepository.Save(movie);
                return StatusCode(201, "Succes");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] MovieDTO movieDTO)
        {
            try
            {
                var movie = _moviesRepository.FindById(movieDTO.Id);
                //check if exist in db
                if (movie == null)
                    return StatusCode(400, "Id Invalido");
                //Update data
                movie.Id = movieDTO.Id;
                movie.Title = movieDTO.Title;
                movie.Image = movieDTO.Image;
                movie.CreationDate = movieDTO.CreationDate;
                movie.Qualification = movieDTO.Qualification;
                movie.GenreId = movieDTO.GenreId;
                movie.Participations = 
                    movieDTO.Characters.Select(per => new Participation
                    {
                        MovieId = movieDTO.Id,
                        CharacterId = per.Id
                    }).ToList();

                //save data
                _moviesRepository.Save(movie);
                return StatusCode(201, "Succes");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var movie = _moviesRepository.FindById(id);
                if (movie == null)
                    return StatusCode(400, "Id Invalido");

                _moviesRepository.DeleteMovie(movie);
                return Ok("Succes");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
