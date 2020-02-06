using JLFilmApi.Context;
using JLFilmApi.Controllers;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo
{
    public class FilmsRepository : IFilmRepository
    {
        private JLDatabaseContext db;

        public FilmsRepository(JLDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Films> GetFilm(int filmId)
        {
            return await db.Films.FirstOrDefaultAsync(x => x.Id == filmId);
        }

        public async Task<FilmAtributtesInformView> GetFilmAttributes()
        {
            var inform = new FilmAtributtesInformView();
            var films = await db.Films.ToListAsync();

            foreach (var item in films)
            {
                if (!inform.FilmTypes.Contains(item.Type))
                {
                    inform.FilmTypes.Add(item.Type);
                }
                if (!inform.FilmCountries.Contains(item.Country))
                {
                    inform.FilmCountries.Add(item.Country);
                }
            }

            return inform;
        }

        public async Task<List<Films>> GetFilms(string value, FilmFilters filterCategory)
        {
            switch (filterCategory)
            {
                case FilmFilters.Name:
                    return await db.Films.Where(x => x.Name == value).ToListAsync();
                case FilmFilters.Country:
                    return await db.Films.Where(x => x.Country == value).ToListAsync();
                case FilmFilters.Type:
                    return await db.Films.Where(x => x.Type == value).ToListAsync();
                default:
                    return await db.Films.ToListAsync();
            }
        }
    }
}
