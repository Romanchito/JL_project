using FluentAssertions;
using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JLFilmApi.IntegrationTests
{
    public class LikeControllerTest : IntegrationTest
    {
        public LikeControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Add_dislike_and_like_to_review_and_check_count_of_likes()
        {
            //Arrange
            ReInitializeDatabase();
            bool isAuthFirstUser = await AuthenticateAsync("user1", "1234");
            
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

                context.Reviews.Add(new Reviews
                {
                    Name = "TestRiview",
                    Text = "TestText",
                    Date = DateTime.Now,
                    UserId = 3,
                    FilmId = 1
                });

                context.SaveChanges();
            }

            var newLikeOfFirstUser = new InfoViewLikes { IsLike = true, ReviewId = 1 };
            var newLikeOfSecondUser = new InfoViewLikes { IsLike = false, ReviewId = 1 };

            //Act
            var responseFirst = await TestClient.PostAsync("api/Likes/newLike/",
                      new StringContent(JsonConvert.SerializeObject(newLikeOfFirstUser),
                      Encoding.UTF8,
                      "application/json"));

            bool isAuthSecondUser = await AuthenticateAsync("user2", "1111");

            var responsSecond = await TestClient.PostAsync("api/Likes/newLike/",
                     new StringContent(JsonConvert.SerializeObject(newLikeOfSecondUser),
                     Encoding.UTF8,
                     "application/json"));


            isAuthFirstUser = await AuthenticateAsync("user1", "1234");
            var responseCountOfLikes = await TestClient.GetAsync("api/Likes/allOfReview/1");
            var listOfLikes = JsonConvert.DeserializeObject<List<InfoViewLikes>>
                (await responseCountOfLikes.Content.ReadAsStringAsync());


            //Assert
            responseFirst.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responsSecond.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseCountOfLikes.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.True(isAuthFirstUser);
            Assert.True(isAuthSecondUser);
            Assert.True(listOfLikes.Count == 2);

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<JLDatabaseContext>();
                var likesArray = await context.Likes.ToArrayAsync();
                var usersArray = await context.Users.ToArrayAsync();               

                Assert.Equal(newLikeOfFirstUser.IsLike, likesArray[0].IsLike);
                Assert.Equal(newLikeOfFirstUser.ReviewId, likesArray[0].ReviewId);
                Assert.True(likesArray[0].UserId == usersArray[0].Id);

                Assert.Equal(newLikeOfSecondUser.IsLike, likesArray[1].IsLike);
                Assert.Equal(newLikeOfFirstUser.ReviewId, likesArray[1].ReviewId);
                Assert.True(likesArray[1].UserId == usersArray[1].Id);
            }
        }
    }
}
