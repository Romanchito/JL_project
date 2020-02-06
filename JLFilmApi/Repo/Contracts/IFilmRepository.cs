using JLFilmApi.Controllers;
using JLFilmApi.DomainModels;
using JLFilmApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IFilmRepository
    {
        Task<List<Films>> GetFilms(string name, FilmFilters filterCategory);
        
        Task<Films> GetFilm(int filmId);

        Task<FilmAtributtesInformView> GetFilmAttributes();
    }
}
