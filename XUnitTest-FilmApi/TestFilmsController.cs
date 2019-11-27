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
        private readonly TestProvider sut;

        public TestFilmsController()
        {
            sut = new TestProvider();

        }

        [Fact]
        public async Task Get_all_films_request()
        {
            var response = await sut.Client.GetAsync("/api/films");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //Get result of getting all films to collection.
            List<InfoViewFilms> resultFilmsList = JsonSerializer.Deserialize<List<InfoViewFilms>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(resultFilmsList);
        }

        [Fact]
        public async Task Get_film_by_id()
        {
            var options = new DbContextOptionsBuilder<JLDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Get_film_by_id")
                .Options;

            using (var context = new JLDatabaseContext(options))
            {
                await context.Films.AddAsync(new Films
                {
                    Name = "film1",
                    Director = "Director",
                    Country = "USA",
                    ReleaseDate = DateTime.Now,
                    Stars = "",
                    WorldwideGross = 100000
                });

                await context.Films.AddAsync(new Films
                {
                    Name = "film2",
                    Director = "Director",
                    Country = "Russia",
                    ReleaseDate = DateTime.Now,
                    Stars = "",
                    WorldwideGross = 12323423
                });

                var response = await sut.Client.GetAsync("/api/films/1");
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            }
        }
    }
}
