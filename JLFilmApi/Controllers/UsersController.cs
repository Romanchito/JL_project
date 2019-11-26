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

            if (await userRepository.GetUserByLogin(user.Login) != null)
            {
                return BadRequest("This user already exists");
            }

            await userRepository.AddUser(mapper.Map<Users>(user));
            return Ok("Add User");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if(await userRepository.GetUserById(id) == null)
            {
                return NotFound();
            }
            await userRepository.DeleteUser(id);
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