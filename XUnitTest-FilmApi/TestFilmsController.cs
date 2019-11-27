using FluentAssertions;
using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest_FilmApi
{
    public class TestFilmsController
    {        
        [Fact]
        public async Task Get_film_all_and_by_id()
        {
            using (var client = new TestProvider().Client)
            {
                var response = await client.GetAsync("/api/films");
                List<InfoViewFilms> resultFilmsList = JsonSerializer.Deserialize<List<InfoViewFilms>>(await response.Content.ReadAsStringAsync());
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                response = await client.GetAsync("/api/films/5");
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                InfoViewFilms resultFilm = JsonSerializer.Deserialize<InfoViewFilms>(await response.Content.ReadAsStringAsync());
                Assert.True(resultFilm.Name == resultFilmsList.ElementAt(0).Name);
            }
        }
    }
}
