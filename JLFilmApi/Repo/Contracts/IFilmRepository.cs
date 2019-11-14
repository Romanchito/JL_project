using JLFilmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IFilmRepository
    {
        Task<List<Films>> GetFilms();
        
        Task<Films> GetFilm(int? filmId);
    }
}
