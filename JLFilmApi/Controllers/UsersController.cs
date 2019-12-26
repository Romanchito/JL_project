using AutoMapper;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        [HttpGet("{login}")]
        public async Task<ActionResult<InfoViewUsers>> GetUser(string login)
        {
            var user = mapper.Map<InfoViewUsers>(await userRepository.GetUserByLogin(login));
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
       
        [HttpPost("newUser")]
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

            int userId = await userRepository.AddUser(mapper.Map<Users>(user));
            return Ok(userId);
        }        

        [HttpPut("updatingUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateViewUsers user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = await userRepository.UpdateUser(mapper.Map<Users>(user), id);
            return Ok(userId);
        }

        [HttpPut("updatingPassword/{id}")]
        public async Task<IActionResult> UpdateUserPassword(int id, UpdatePasswordView passwordView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = await userRepository.UpdateAccountPassword(passwordView.Password, id);
            return Ok(userId);
        }
    }
}