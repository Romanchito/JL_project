using JLFilmApi.Models;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Mvc;
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
            var films = await filmRepository.GetFilms();
            if (films == null)
            {
                return new List<Films>();
            }
            return films;
        }

        [HttpGet("{id}")]
        public async Task<Films> GetFilm(int? id)
        {
            var film = await filmRepository.GetFilm(id);
            return film;
        }
    }
}

