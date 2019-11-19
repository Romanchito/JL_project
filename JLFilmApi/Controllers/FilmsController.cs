using JLFilmApi.Models;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private IFilmRepository filmRepository;

        public FilmsController(IFilmRepository filmRepository)
        {
            this.filmRepository = filmRepository;
        }


        [HttpGet]
        public async Task<List<Films>> GetFilms()
        {
            throw new NullReferenceException();
            var films = await filmRepository.GetFilms();
            if (films == null)
            {
                return new List<Films>();
            }
            return films;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Films>> GetFilm(int? id)
        {
            var film = await filmRepository.GetFilm(id);
            if (film == null) return NotFound("Sorry, but this film doesn't exist :" + id.ToString());
            return film;
        }
    }
}

