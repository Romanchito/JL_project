using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JL_project.Models;
using Microsoft.Data.SqlClient;

namespace JL_project.DataProvider
{
    public class FilmDataProvider : IFilmDataProvider
    {
        private readonly string connectionString = @"Server=RTRETYAKOV\SQLEXPRESS;Database=JLDatabase;Trusted_Connection=True;";
        private SqlConnection sqlConnection;
        public Task<Film> GetFilm()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Film>> GetFilms()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<Film>(
                    "SELECT * FROM FILMS",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
