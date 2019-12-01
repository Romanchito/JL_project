using JLFilmApi.Context;
using JLFilmApi.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace JLFilmApi.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        public IServiceProvider ServiceProvider { get; set; }

        private static void ReplaceCoreServices<TContextImplementation>(IServiceCollection serviceCollection,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction,
            ServiceLifetime optionsLifetime) where TContextImplementation : DbContext
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(DbContextOptions<TContextImplementation>),
                (IServiceProvider p) => DbContextOptionsFactory<TContextImplementation>(p, optionsAction), optionsLifetime));
            serviceCollection.Add(new ServiceDescriptor(typeof(DbContextOptions),
                (IServiceProvider p) => p.GetRequiredService<DbContextOptions<TContextImplementation>>(), optionsLifetime));
        }

        private static DbContextOptions<TContext> DbContextOptionsFactory<TContext>(IServiceProvider applicationServiceProvider,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction) where TContext : DbContext
        {
            DbContextOptionsBuilder<TContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<TContext>(
                new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>()));
            dbContextOptionsBuilder.UseApplicationServiceProvider(applicationServiceProvider);
            optionsAction?.Invoke(applicationServiceProvider, dbContextOptionsBuilder);
            return dbContextOptionsBuilder.Options;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                ReplaceCoreServices<JLDatabaseContext>(services, (p, o) =>
                {
                    o.UseInMemoryDatabase("DB").UseLazyLoadingProxies(); ;
                }, ServiceLifetime.Scoped);

                var sp = services.BuildServiceProvider();
                ServiceProvider = sp;

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
