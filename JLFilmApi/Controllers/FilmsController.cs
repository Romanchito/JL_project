using AutoMapper;
using JLFilmApi.Helpers;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    public enum FilmFilters { Name, Country, Type }
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private IFilmRepository filmRepository;
        private IBinaryResourcePathResolver resourcePathResolver;
        private IMapper mapper;

        public FilmsController(IFilmRepository filmRepository, IMapper mapper,
            IBinaryResourcePathResolver resourcePathResolver)
        {
            this.mapper = mapper;
            this.resourcePathResolver = resourcePathResolver;
            this.filmRepository = filmRepository;
        }

        [HttpGet("Name/{name?}")]
        public async Task<List<InfoViewFilms>> GetFilmsByName(string name = "")
        {
            var films = mapper.Map<List<InfoViewFilms>>(await filmRepository.GetFilms(name, FilmFilters.Name));
            await GetImagesForFilms(films);
            return films;
        }

        [HttpGet("Country/{country}")]
        public async Task<List<InfoViewFilms>> GetFilmsByCountry(string country)
        {
            var films = mapper.Map<List<InfoViewFilms>>(await filmRepository.GetFilms(country, FilmFilters.Country));
            await GetImagesForFilms(films);
            return films;
        }


        [HttpGet("Type/{type}")]
        public async Task<List<InfoViewFilms>> GetFilmsByType(string type)
        {
            var films = mapper.Map<List<InfoViewFilms>>(await filmRepository.GetFilms(type, FilmFilters.Type));
            await GetImagesForFilms(films);
            return films;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InfoViewOneFilm>> GetFilm(int id)
        {
            var film = mapper.Map<InfoViewOneFilm>(await filmRepository.GetFilm(id));
            if (film == null) return NotFound("Sorry, but this film doesn't exist :" + id.ToString());
            if (film.FilmImage == null) film.FilmImage = ImageDefaultNames.DEFAULT_FILM_IMAGE_NAME;
            film.FilmImageUrl = await resourcePathResolver.Take(new TakingImageModel(Types.Film, film.FilmImage));
            return film;
        }

        private async Task GetImagesForFilms(List<InfoViewFilms> films)
        {
            foreach (var item in films)
            {
                if (item.FilmImage == null) item.FilmImage = ImageDefaultNames.DEFAULT_FILM_IMAGE_NAME;
                item.FilmImageUrl = await resourcePathResolver.Take(new TakingImageModel(Types.Film, item.FilmImage));
            }
        }

        [HttpGet("Inform/")]
        public async Task<ActionResult<FilmAtributtesInformView>> GetInformOfFilmsAttributes()
        {
            return await filmRepository.GetFilmAttributes();
        }


    }
}

