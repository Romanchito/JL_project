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

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoViewUsers>> GetUser(int id)
        {
            var user = await userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewUser(AddViewUsers user)
        {
            if (ModelState.IsValid)
            {
                int userId = await userRepository.AddUser(user);
                if (userId >= 0)
                {
                    return Ok("Add User");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest(ModelState);
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
        public async Task<IActionResult> UpdateUser(int id , UpdateViewUsers user)
        {            
            await userRepository.UpdateUser(user,id);
            return Ok();
        }
    }
}