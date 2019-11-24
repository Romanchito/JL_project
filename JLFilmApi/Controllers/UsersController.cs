using AutoMapper;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserRepository userRepository;
        private IMapper mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoViewUsers>> GetUser(int id)
        {
            var user = mapper.Map<InfoViewUsers>(await userRepository.GetUserById(id));
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewUser(AddViewUsers user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = await userRepository.AddUser(mapper.Map<Users>(user));

            return Ok("Add User");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            int? result = await userRepository.DeleteUser(id);
            if (result == 0 || result == null)
            {
                return NotFound(result);
            }
            return Ok();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateViewUsers user)
        {            
            await userRepository.UpdateUser(mapper.Map<Users>(user), id);
            return Ok();
        }
    }
}