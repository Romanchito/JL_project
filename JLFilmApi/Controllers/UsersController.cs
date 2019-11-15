using JLFilmApi.Models;
using JLFilmApi.Repo.Contracts;
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
        public async Task<ActionResult<Users>> GetUser(int id)
        {

            var user = await userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(Users user)
        {
            if (ModelState.IsValid)
            {
                int userId = await userRepository.AddUser(user);
                if (userId > 0)
                {
                    return Ok("Add User");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest(user);
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
        public async Task<IActionResult> UpdateUser(Users user)
        {
            await userRepository.UpdateUser(user);
            return Ok();
        }
    }
}