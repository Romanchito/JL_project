using AutoMapper;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private IBinaryResourcePathResolver resourcePathResolver;
        private IMapper mapper;

        public FilmsController(IFilmRepository filmRepository, IMapper mapper,
            IBinaryResourcePathResolver resourcePathResolver)
        {
            this.mapper = mapper;
            this.resourcePathResolver = resourcePathResolver;
            this.filmRepository = filmRepository;
        }

        [HttpGet]
        public async Task<List<InfoViewFilms>> GetFilms()
        {
            var films = mapper.Map<List<InfoViewFilms>>(await filmRepository.GetFilms());
            //  films = await AddImagePathToFilms(films);

            foreach (var item in films)
            {
                if (item.FilmImage != null)
                {
                    item.FilmImageUrl = await resourcePathResolver.Take(new TakingImageModel("film", item.FilmImage));

                }
            }
            return films;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InfoViewOneFilm>> GetFilm(int id)
        {
            var film = mapper.Map<InfoViewOneFilm>(await filmRepository.GetFilm(id));
            if (film == null) return NotFound("Sorry, but this film doesn't exist :" + id.ToString());
            return film;
        }

        //private async Task<List<InfoViewFilms>> AddImagePathToFilms(List<InfoViewFilms> films)
        //{
        //    var list = films;
        //    foreach (var item in list)
        //    {
        //        item.FilmImageUrl = await resourcePathResolver.Take(new TakingImageModel("film", item.FilmImage));
        //    }
        //    return list;
        //}
    }
}

