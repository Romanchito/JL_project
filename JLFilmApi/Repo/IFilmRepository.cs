using JLFilmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo
{
    public interface IFilmRepository
    {
        Task<List<Film>> GetFilms();
        
        Task<Film> GetFilm(Guid? filmId);
    }
}
