using FluentAssertions;
using JLFilmApi.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JLFilmApi.IntegrationTests
{
    public class UserControllerTest : IntegrationTest
    {
        [Fact]
        public async Task Add_new_User()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await TestClient.PostAsync("/api/Users/newUser", new StringContent
                (
                    JsonConvert.SerializeObject( 
                            new AddViewUsers 
                            {
                                Login = "Login",
                                Name = "Name",
                                Password = "wwww",
                                Surname = "Surname",
                                AccountImage = "
                            }  
                        )
                ));
            string newUserId = await response.Content.ReadAsStringAsync();
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            
        }
    }
}
