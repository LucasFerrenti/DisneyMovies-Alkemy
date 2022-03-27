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
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var genres = _genreRepository.FindAll().OrderBy(gen => gen.Name).ToList();
                var view = genres.Select(gen => new GenreDTO
                {
                    Id = gen.Id,
                    Name = gen.Name,
                    Image = gen.Image
                }).ToList();
                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] AddGenreRequest request)
        {
            try
            {
                var genre = new Genre
                {
                    Name = request.Name,
                    Image = request.Image
                };
                _genreRepository.Save(genre);
                return StatusCode(200, "Succes");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(GenreDTO genreDTO)
        {
            try
            {
                var genre = _genreRepository.FindById(genreDTO.Id);
                if (genre == null)
                    return StatusCode(400, "Id invalido");
                genre.Name = genreDTO.Name;
                genre.Image = genreDTO.Image;
                _genreRepository.Save(genre);
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
                var genre = _genreRepository.FindById(id);
                if (genre == null)
                    return StatusCode(400, "Id Invalido");
                _genreRepository.DeleteGenre(genre);
                return Ok("Succes");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
