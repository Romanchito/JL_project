using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JL_project.DataProvider;
using JL_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace JL_project.Controllers
{
    [Route("api/[controller]")]
    public class FilmController : Controller
    {
        private IFilmDataProvider filmDataProvider;

        public FilmController(IFilmDataProvider filmDataProvider)
        {
            this.filmDataProvider = filmDataProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<Film>> Get()
        {
            return await this.filmDataProvider.GetFilms();
        }
    }
}