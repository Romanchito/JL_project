using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JLFilmApi.Repo
{
    public class FilmsRepository : IFilmRepository
    {
        private JLDatabaseContext db;
        private readonly IMapper filmMapper;

        public FilmsRepository(JLDatabaseContext db, IMapper mapper)
        {
            this.db = db;
            filmMapper = mapper;
        }

        public async Task<InfoViewOneFilm> GetFilm(int? filmId)
        {
            InfoViewOneFilm infoViewFilms = filmMapper.Map<InfoViewOneFilm>( await db.Films.FirstOrDefaultAsync(x => x.Id == filmId));
            return infoViewFilms;
        }

        public async Task<List<InfoViewFilms>> GetFilms()
        {
            List<InfoViewFilms> list = filmMapper.Map<List<InfoViewFilms>>(await db.Films.ToListAsync());
            return list;
        }
    }
}
