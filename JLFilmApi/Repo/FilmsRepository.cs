using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Films>> GetFilms()
        {
           return await db.Films.ToListAsync();            
        }
    }
}
