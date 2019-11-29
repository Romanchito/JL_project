using System;
using JLFilmApi.Context;
using JLFilmApi.IntegrationTests.Helpers;
using JLFilmApi.Repo;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace JLFilmApi.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.RemoveAll(typeof(JLDatabaseContext));
                services.AddScoped<IUserRepository, UsersRepository>();
                
                services.AddDbContext<JLDatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName: "TestingDatabase");
                    options.UseInternalServiceProvider(serviceProvider);
                });



                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<JLDatabaseContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();


                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        DataUtilities.ReInitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}
