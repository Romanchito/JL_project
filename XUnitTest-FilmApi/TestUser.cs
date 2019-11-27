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
    public class TestUser
    {
        [Fact]
        public async Task Add_new_user_to_database()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<JLDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Add_new_user_to_database")
                .Options;

            //Act
            using (var context = new JLDatabaseContext(options))
            {
                var userRepo = new UsersRepository(context);
                var newUser = new Users { Name = "Name", Surname = "Surname", Login = "Login", Password = "Password" };
;               await userRepo.AddUser(newUser);
            }

            //Assert
            using (var context = new JLDatabaseContext(options))
            {
                Assert.Equal(1, (await context.Users.CountAsync()) );
                Assert.Equal("Name", (await context.Users.SingleAsync()).Name );
            }

        }

        [Fact]
        public async Task Update_user_database()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<JLDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Update_user_database")
                .Options;

            //Act
            using (var context = new JLDatabaseContext(options))
            {
                var userRepo = new UsersRepository(context);
                var updateUser = new Users { Name = "Name", Surname = "Surname", Login = "Login", Password = "Password" };

                await userRepo.AddUser(updateUser);
                updateUser.Name = "UpdateName";               
                await userRepo.UpdateUser(updateUser, 1);
            }

            //Assert
            using (var context = new JLDatabaseContext(options))
            {
                Assert.Equal(1, (await context.Users.CountAsync()));
                Assert.Equal("UpdateName", (await context.Users.SingleAsync()).Name);
            }

        }

        [Fact]
        public async Task Delete_user_database()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<JLDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Delete_user_database")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            //Act
            using (var context = new JLDatabaseContext(options))
            {
                var userRepo = new UsersRepository(context);
                var updateUser = new Users { Name = "Name", Surname = "Surname", Login = "Login", Password = "Password" };

                await userRepo.AddUser(updateUser);                
                await userRepo.DeleteUser(1);
            }

            //Assert
            using (var context = new JLDatabaseContext(options))
            {
                Assert.Equal(0, (await context.Users.CountAsync()));                
            }

        }

        [Fact]
        public async Task Get_existing_and_notexisting_user_by_id_from_database()
        {
            //Array
            var options = new DbContextOptionsBuilder<JLDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Delete_user_database")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            //Act
            using (var context = new JLDatabaseContext(options))
            {
                var userRepo = new UsersRepository(context);

                //Create and add two users to database
                var user1 = new Users { Name = "User1", Surname = "Surname", Login = "Login", Password = "Password" };
                var user2 = new Users { Name = "User2", Surname = "Surname", Login = "Login", Password = "Password" };
                await userRepo.AddUser(user1);
                await userRepo.AddUser(user2);

                //Delete user1
                await userRepo.DeleteUser(1);

                //Update user2's name
                user2.Name = "SecondUser";
                await userRepo.UpdateUser(user2,2);
            }

            //Assert
            using (var context = new JLDatabaseContext(options))
            {
                var userRepo = new UsersRepository(context);
                Assert.Equal("SecondUser", (await userRepo.GetUserById(2)).Name);
                Assert.Null(await userRepo.GetUserById(1));
            }

        }
    }
}
