using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
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
        public async Task<List<InfoViewFilms>> GetFilms()
        {
            var films = await filmRepository.GetFilms();
            if (films == null)
            {
                return new List<InfoViewFilms>();
            }
            return films;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InfoViewOneFilm>> GetFilm(int? id)
        {
            var film = await filmRepository.GetFilm(id);
            if (film == null) return NotFound("Sorry, but this film doesn't exist :" + id.ToString());
            return film;
        }
    }
}

