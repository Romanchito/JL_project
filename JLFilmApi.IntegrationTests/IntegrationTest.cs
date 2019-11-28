using JLFilmApi.Context;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JLFilmApi.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        public IntegrationTest()
        {
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(buldier =>
                    {
                        buldier.ConfigureServices(services =>
                        {
                            services.RemoveAll(typeof(JLDatabaseContext));
                            services.AddDbContext<JLDatabaseContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                        });
                    }
                );
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
