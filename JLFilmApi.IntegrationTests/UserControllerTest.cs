using FluentAssertions;
using JLFilmApi.Context;
using JLFilmApi.IntegrationTests.Helpers;
using JLFilmApi.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JLFilmApi.IntegrationTests
{
    public class UserControllerTest : IntegrationTest
    {
        public UserControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Add_new_User_and_check_auth()
        {
            ReInitializeDatabase();
            //Arrange
            AddViewUsers addUser = new AddViewUsers
            {
                Login = "Login",
                Name = "Name",
                Password = "wwww",
                Surname = "Surname",
                AccountImage = ""
            };            

            //Act
            var response = await TestClient.PostAsync("/api/Users/newUser",
                    new StringContent(JsonConvert.SerializeObject(addUser),
                    Encoding.UTF8,
                    "application/json"));
            var isAuth = await AuthenticateAsync(addUser.Login, addUser.Password);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.True(isAuth);
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<JLDatabaseContext>();
                var actual = context.Users.Single(u => u.Login == addUser.Login);

                Assert.Equal(addUser.Name, actual.Name);
                Assert.Equal(addUser.Password, actual.Password);
                Assert.Equal(addUser.Surname, actual.Surname);
                Assert.Empty(actual.AccountImage);
            }

        }

        [Fact]
        public async Task Update_and_get_by_id_user()
        {
            //Arrange
           
            AddViewUsers addUser = new AddViewUsers
            {
                Login = "Login",
                Name = "Name",
                Password = "wwww",
                Surname = "Surname",
                AccountImage = ""
            };
            UpdateViewUsers updateUser = new UpdateViewUsers
            {
                Name = "UpdateName",
                Password = "UpdatePassword",
                Surname = "UpdateSurname"
            };

            //Act
            var addResponse = await TestClient.PostAsync("/api/Users/newUser",
                   new StringContent(JsonConvert.SerializeObject(addUser),
                   Encoding.UTF8,
                   "application/json"));


            int userId = serviceProvider.CreateScope()
                        .ServiceProvider
                        .GetRequiredService<JLDatabaseContext>()
                        .Users.Single(u => u.Login == addUser.Login).Id;

            var updateResponse = await TestClient.PutAsync("/api/Users/updatingUser/" + userId,
                    new StringContent(JsonConvert.SerializeObject(updateUser),
                    Encoding.UTF8,
                    "application/json"));

            var gettingResponse = await TestClient.GetAsync("/api/Users/" + userId);
            var resultUser = JsonConvert.DeserializeObject<InfoViewUsers>(await gettingResponse.Content.ReadAsStringAsync());

            //Assert
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<JLDatabaseContext>();

                string resultUserPassword = context.Users.Single(u => u.Login == addUser.Login).Password;

                updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                gettingResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                Assert.Equal(resultUser.Name, updateUser.Name);
                Assert.Equal(resultUserPassword, updateUser.Password);
                Assert.Equal(resultUser.Surname, updateUser.Surname);
            }

        }

    }
}
