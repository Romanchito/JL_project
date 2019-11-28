using FluentAssertions;
using JLFilmApi.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;


namespace JLFilmApi.IntegrationTests
{
    public class FilmsControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetAll_Films()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await TestClient.GetAsync("/api/Films");
            List<InfoViewFilms> listOfFilms = JsonConvert.DeserializeObject<List<InfoViewFilms>>(await response.Content.ReadAsStringAsync());
            InfoViewFilms fim = listOfFilms.ElementAt(0);
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.True(listOfFilms.Count > 0);
        }

        [Fact]
        public async Task Get_Film_by_id()
        {
            //Arrange            

            //Act
            var response = await TestClient.GetAsync("/api/Films/5");
            InfoViewFilms resultFilm = JsonConvert.DeserializeObject<InfoViewFilms>(await response.Content.ReadAsStringAsync());
            string flg = resultFilm.Country;
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Assert.NotNull(resultFilm);
            Assert.True(resultFilm.Country == "England");
        }
    }
}
