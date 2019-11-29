using JLFilmApi.Context;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace JLFilmApi.IntegrationTests
{
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly HttpClient TestClient;
        private readonly CustomWebApplicationFactory<Startup> myFactory;

        public IntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            myFactory = factory;
            TestClient = factory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var responce = TestClient.PostAsJsonAsync("/jwtToken", new AuthModel
            {
                Username = "user1",
                Password = "1234"
            });

            var result = (await responce).Content;

            return await result.ReadAsStringAsync();
        }
    }
}
