using AutoMapper;
using JLFilmApi.Context;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NuGet.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JLFilmApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
         
        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<JLDatabaseContext>(item =>
                    {
                       item.UseSqlServer(Configuration.GetConnectionString("MyDBConnection")).UseLazyLoadingProxies();                   
                    }
                );            
            services.AddScoped<IFilmRepository, FilmsRepository>();
            services.AddScoped<IUserRepository, UsersRepository>();
            services.AddScoped<IReviewsRepository, ReviewsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IBinaryResourcePathResolver, SolutionBinaryResourceResolver>();           
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());               
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oath2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                }
                    );
            });
            services.AddRouting();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(bearer =>
                   {
                       bearer.RequireHttpsMetadata = false;
                       bearer.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = AuthOptions.ISSUER,
                           ValidAudience = AuthOptions.AUDIENCE,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY))
                       };
                   });           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


        }
    }
}
