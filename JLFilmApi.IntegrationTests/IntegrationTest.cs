﻿using JLFilmApi.Context;
using JLFilmApi.IntegrationTests.Helpers;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace JLFilmApi.IntegrationTests
{
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected HttpClient TestClient;
        protected IServiceProvider serviceProvider;

        private CustomWebApplicationFactory<Startup> factory;

        public IntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {            
            TestClient = factory.CreateClient();
            this.factory = factory;
            this.TestClient = factory.CreateClient();
            serviceProvider = factory.ServiceProvider;

        }      

        protected async Task<bool> AuthenticateAsync(string name, string password)
        {
            string token = await GetJwtAsync(name, password);
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (token == null) return false;
            return true;
        }

        protected void ReInitializeDatabase()
        {
            var db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<JLDatabaseContext>();
            try
            {
                DataUtilities.ReInitializeDbForTests(db);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GetJwtAsync(string name, string password)
        {
            var responce = TestClient.PostAsJsonAsync("/jwtToken", new AuthModel
            {
                Username = name,
                Password = password
            });

            var result = (await responce).Content;

            return await result.ReadAsStringAsync();
        }

        
    }
}
