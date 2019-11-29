using FluentAssertions;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Newtonsoft.Json;
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
            
            //Array
            AddViewUsers addUser = new AddViewUsers
            {
                Login = "Login",
                Name = "Name",
                Password = "wwww",
                Surname = "Surname",
                AccountImage = ""
            };

            //Act
            var response = await TestClient.PostAsync("/api/Users/newUser", new StringContent(JsonConvert.SerializeObject(addUser), Encoding.UTF8, "application/json"));
            string s = JsonConvert.SerializeObject(addUser);
            var isAuth = await AuthenticateAsync(addUser.Login, addUser.Password);
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.True(isAuth);

        }
        
    }
}
