using AutoMapper;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
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

        [HttpPost("jwtToken")]
        public async Task<ActionResult<string>> Token(AuthModel authModel)
        {
            
            var identity = await GetIdentityAsync(authModel.Username, authModel.Password);
           
            if(identity == null)
            {
                ModelState.AddModelError("Username", "Неверно введенный адрес или пароль");
                return BadRequest(ModelState);
            }

            var jwtToken = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: identity.Claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
           
            var response = new { jwtHandler = new JwtSecurityTokenHandler().WriteToken(jwtToken) };          
        
            return Ok(response);

        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string login, string password)
        {
            InfoViewUsers user = await CheckingUserAsync(login, password);

            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Login),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }

        private async Task<InfoViewUsers> CheckingUserAsync(string login, string password)
        {
            AddViewUsers user = mapper.Map<AddViewUsers>(await userRepository.GetUserByLogin(login));
            if (user == null || !user.Password.Equals(password))
            {
                return null;
            }
            return mapper.Map<InfoViewUsers>(user);
        }
    }
}