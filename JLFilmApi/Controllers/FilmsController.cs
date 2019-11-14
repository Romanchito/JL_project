using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JLFilmApi.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JLFilmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private IFilmRepository filmRepository;

        public FilmsController(IFilmRepository filmRepository)
        {
            this.filmRepository = filmRepository;
        }

        [HttpGet]
        [Route("GetFilms")]
        public async Task<IActionResult> GetFilms()
        {
            try
            {
                var films = await filmRepository.GetFilms();
                if (films == null)
                {
                    return NotFound();
                }

                return Ok(films);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetFilm")]
        public async Task<IActionResult> GetFilm(Guid? id)
        {
            try
            {
                var film = await filmRepository.GetFilm(id);
                if (film == null)
                {
                    return NotFound();
                }

                return Ok(film);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}