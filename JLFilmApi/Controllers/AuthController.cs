using AutoMapper;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository userRepository;
        private IMapper mapper;

        public AuthController(IUserRepository userRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        [HttpPost("/getJwtToken")]
        public async Task Token(AuthModel authModel)
        {
            var identity = await GetIdentityAsync(authModel.Username, authModel.Password);

            var now = DateTime.UtcNow;

            var jwtToken = new JwtSecurityToken(

                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)

                );

            var jwtHandler = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var response = new { access_token = jwtHandler, username = identity.Name };
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));

        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string login, string password)
        {
            InfoViewUsers user = await CheckingUserAsync(login, password);

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }

        private async Task<InfoViewUsers> CheckingUserAsync(string login, string password)
        {
            InfoViewUsers user = mapper.Map<InfoViewUsers>(await userRepository.GetUserByLogin(login));
            if (user == null || !user.Password.Equals(password))
            {
                throw new NullReferenceException("Incorrect login or password");
            }            
            return user;
        }
    }
}