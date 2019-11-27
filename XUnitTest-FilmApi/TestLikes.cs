using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest_FilmApi
{
    public class TestLikes
    {
        [Fact]
        public async Task Add_new_like_to_review()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<JLDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Add_new_like_to_review")
                .Options;

            //Act
            using (var context = new JLDatabaseContext(options))
            {
                //Add User
                var userRepo = new UsersRepository(context);
                var newUser = new Users { Name = "Name", Surname = "Surname", Login = "Login", Password = "Password" };
                await userRepo.AddUser(newUser);

                //add Film
                await context.Films.AddAsync(new Films
                {
                    Name = "Name",
                    Director = "Director",
                    Country = "USA",
                    ReleaseDate = DateTime.Now,
                    Stars = "",
                    WorldwideGross = 1000
                });

                //Add Review
                var reviewRepo = new ReviewsRepository(context);
                var newReview = new Reviews { Name = "ReviewName", Text = "Text", Date = DateTime.Now, FilmId = 1, UserId = 1 };
                await reviewRepo.AddReview(newReview);

                //Add Like
                var likeRepo = new LikesRepository(context);
                var newLike = new Likes { Type = false, ReviewId = 1, UserId = 1 };
                await likeRepo.AddNewLike(newLike);
            }

            //Assert
            using (var context = new JLDatabaseContext(options))
            {
                Assert.Equal(1, (await context.Likes.CountAsync()));
                Assert.False((await context.Likes.ToListAsync()).ElementAt(0).Type);
                Assert.Equal(1, (await context.Reviews.SingleAsync()).Likes.Count);
                Assert.Equal(1, (await context.Users.SingleAsync()).Likes.Count);
                
            }

        }

        [Fact]
        public async Task Delete_likes_when_delete_users()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<JLDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Add_new_like_to_review")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            //Act
            using (var context = new JLDatabaseContext(options))
            {
                //Add User
                var userRepo = new UsersRepository(context);
                var newUser = new Users { Name = "Name", Surname = "Surname", Login = "Login", Password = "Password" };
                await userRepo.AddUser(newUser);

                //add Film
                await context.Films.AddAsync(new Films
                {
                    Name = "Name",
                    Director = "Director",
                    Country = "USA",
                    ReleaseDate = DateTime.Now,
                    Stars = "",
                    WorldwideGross = 1000
                });

                //Add Review
                var reviewRepo = new ReviewsRepository(context);
                var newReview = new Reviews { Name = "ReviewName", Text = "Text", Date = DateTime.Now, FilmId = 1, UserId = 1 };
                await reviewRepo.AddReview(newReview);

                //Add Like
                var likeRepo = new LikesRepository(context);
                var newLike = new Likes { Type = false, ReviewId = 1, UserId = 1 };
                await likeRepo.AddNewLike(newLike);
                //Delete User
                await userRepo.DeleteUser(1);
            }

            //Assert
            using (var context = new JLDatabaseContext(options))
            {
                Assert.Equal(0, (await context.Likes.CountAsync()));               
                Assert.Equal(0, await context.Reviews.CountAsync());
                Assert.Equal(0, await context.Users.CountAsync());

            }



        }

    }
}
