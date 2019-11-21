using JLFilmApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IFilmRepository
    {
        Task<List<InfoViewFilms>> GetFilms();
        
        Task<InfoViewOneFilm> GetFilm(int? filmId);
    }
}
