using FluentAssertions;
using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace JLFilmApi.IntegrationTests
{
    public class FilmsControllerTest : IntegrationTest
    {
        public FilmsControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            ReInitializeDatabase();
        }

        [Fact]
        public async Task GetAll_Films()
        {
            //Arrange         
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<JLDatabaseContext>();

                context.Films.Add(new Films
                {
                    Name = "TestRiview",
                    Director = "Spilberg",
                    Country = "USA",
                    Stars = "Deny Devitto, Add Rush",
                    ReleaseDate = DateTime.ParseExact("2009-05-08", "yyyy-MM-dd",
                                  System.Globalization.CultureInfo.InvariantCulture),
                    WorldwideGross = 75000000
                });

                context.Films.Add(new Films
                {
                    Name = "Moscow doesn't believe cry",
                    Director = "Mihalkov",
                    Country = "USSR",
                    Stars = "Muravieva, Lyadov",
                    ReleaseDate = DateTime.ParseExact("1985-05-08", "yyyy-MM-dd",
                                   System.Globalization.CultureInfo.InvariantCulture),
                    WorldwideGross = 980000
                });

                context.SaveChanges();
            }
            //Act
            var response = await TestClient.GetAsync("/api/Films");
            List<InfoViewFilms> listOfFilms = JsonConvert.DeserializeObject<List<InfoViewFilms>>(await response.Content.ReadAsStringAsync());
            
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.True(listOfFilms.Count == 2);
        }

        [Fact]
        public async Task Get_Film_by_id()
        {
            //Arrange     
            
            int filmId;
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<JLDatabaseContext>();

                context.Films.Add(new Films
                {
                    Name = "TestRiview",
                    Director = "Spilberg",
                    Country = "USA",
                    Stars = "Deny Devitto, Add Rush",
                    ReleaseDate = DateTime.ParseExact("2009-05-08", "yyyy-MM-dd",
                                  System.Globalization.CultureInfo.InvariantCulture),
                    WorldwideGross = 75000000
                });

                context.SaveChanges();

                filmId = context.Films.Single().Id;
            }
                //Act
            var response = await TestClient.GetAsync("/api/Films/" + filmId);
            InfoViewFilms resultFilm = JsonConvert.DeserializeObject<InfoViewFilms>(await response.Content.ReadAsStringAsync());
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.NotNull(resultFilm);            
        }
    }
}
