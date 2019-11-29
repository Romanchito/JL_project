using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace JLFilmApi.IntegrationTests
{
    public class LikeControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient myClient;
        private CustomWebApplicationFactory<Startup> factory;        

        public LikeControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            myClient = factory.CreateClient();
            this.factory = factory;
        }

        [Fact]
        public async Task Add_Dislike()
        {
            var response = await myClient.GetAsync("/api/Films");
            List<InfoViewFilms> listOfFilms = JsonConvert.DeserializeObject<List<InfoViewFilms>>(await response.Content.ReadAsStringAsync());
            
        }
    }
}
