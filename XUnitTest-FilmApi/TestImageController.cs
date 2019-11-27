using FluentAssertions;
using JLFilmApi.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest_FilmApi
{
    public class TestImageController
    {
        
        
        [Fact]
        public async Task Get_Authorise_User_AccountImage()
        {
            using (var client = new TestProvider().Client)
            {
                var response = await client.PostAsync("/getJwtToken", new StringContent(
                    JsonConvert.SerializeObject (new AuthModel() { Username = "user1", Password = "1234" }),
                    Encoding.UTF8,
                    "application/json"
                    ));
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                
            }
        }
    }
}
