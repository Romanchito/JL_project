using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JLFilmApi.Context;
using JLFilmApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JLFilmApi.Repo
{
    public class FilmRepository : IFilmRepository
    {
        private JLDatabaseContext db;

        public FilmRepository(JLDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Films> GetFilm(int? filmId)
        {
            if(db != null)
            {
                return await db.Films.FirstOrDefaultAsync(x=>x.Id == filmId);
            }
            return null;
        }

        public async Task<List<Films>> GetFilms()
        {
            if (db != null)
            {
                return await db.Films.ToListAsync(); ;
            }
            return null;
        }
    }
}
