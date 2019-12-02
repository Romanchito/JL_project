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
    public class ImageControllerTest : IntegrationTest
    {
        public ImageControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }


        [Fact]
        public async Task Get_film_image()
        {
            //Arrange
            ReInitializeDatabase();
            int filmId;
            string filmImageName;
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
                    WorldwideGross = 75000000,
                    FilmImage = "img.jpg"
                });

                context.SaveChanges();

                filmId = context.Films.Single().Id;
                filmImageName = context.Films.Single().FilmImage;
            }

            //Act
            var response = await TestClient.GetAsync("api/Image/filmImage/" + filmId);
            string imagePath = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.NotNull(imagePath);
            Assert.Contains(filmImageName, imagePath);
        }

        [Fact]
        public async Task Get_user_image()
        {
            //Arrange
                      
            string userImageName;
            bool isAuth;
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<JLDatabaseContext>();             

                context.SaveChanges();
                var user = context.Users.First();
                userImageName = user.AccountImage;
                isAuth = await AuthenticateAsync(user.Login, user.Password);

            }

            //Act
            var response = await TestClient.GetAsync("api/Image/userImage/");
            string imagePath = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.True(isAuth);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);            
            Assert.NotNull(imagePath);
            Assert.Contains(userImageName, imagePath);
        }
    }
}
