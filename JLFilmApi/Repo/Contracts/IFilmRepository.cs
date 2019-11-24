using JLFilmApi.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IFilmRepository
    {
        Task<List<Films>> GetFilms();
        
        Task<Films> GetFilm(int? filmId);
    }
}
