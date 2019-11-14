using JL_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JL_project.DataProvider
{
    public interface IFilmDataProvider
    {
        Task<IEnumerable<Film>> GetFilms();
        Task<Film> GetFilm();
    }
}
