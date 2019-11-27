using JLFilmApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace XUnitTest_FilmApi
{
    class TestProvider : IDisposable
    {        
        public HttpClient Client { get; set; }
        private TestServer testServer;
        
        public TestProvider()
        {
            SetupClient();
        }

        private void SetupClient()
        {
            testServer = new TestServer(new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()
                        .SetBasePath(@"C:\Users\rtretyakov\Source\Repos\Romanchito\JL_project\JLFilmApi")
                        .AddJsonFile("appsettings.json")
                        .Build()
                )
                .UseStartup<Startup>());
                
            Client = testServer.CreateClient();
        }

        public void Dispose()
        {
            testServer?.Dispose();
            Client?.Dispose();
        }
    }
}
