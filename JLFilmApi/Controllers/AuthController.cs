using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Collections.Generic;
using JLFilmApi.Models;
using System.Linq;

namespace JLFilmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("/jwtToken")]
        public async Task Token(AuthModel authModel)
        {
            var identity = await GetIdentityAsync(authModel.Username, authModel.Password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password");
                return;
            }
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
            var users = await userRepository.GetAllUsers();
            Users user = users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}