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
        private MyDbContext db;

        public FilmRepository(MyDbContext db)
        {
            this.db = db;
        }

        public async Task<Film> GetFilm(Guid? filmId)
        {
            if(db != null)
            {
                return db.Films.FirstOrDefault(x=>x.Id == filmId);
            }
            return null;
        }

        public async Task<List<Film>> GetFilms()
        {
            if (db != null)
            {
                return await db.Films.ToListAsync(); ;
            }
            return null;
        }
    }
}
