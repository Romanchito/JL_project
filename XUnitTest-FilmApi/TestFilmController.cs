using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest_FilmApi
{
    public class TestFilmController
    {
        [Fact]
        public async Task Test_get_all_films()
        {
            var client = new TestProvider().Client;

            var response = await client.GetAsync("/api/films");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Test_get_films_by_id()
        {
            var client = new TestProvider().Client;
            var response = await client.GetAsync("/api/films/1");            
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
