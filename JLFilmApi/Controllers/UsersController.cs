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
            if (!await IsUserExist(user.Login))
            {
                int userId = await userRepository.AddUser(mapper.Map<Users>(user));
                return Ok(userId);
            }

            var jsonResponse =
                new
                {
                    errors = new
                    {
                        general = new[] { "user user with this email is existing" }
                    }
                };
            return BadRequest(jsonResponse);           
        }        

        [HttpPut("updatingUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateViewUsers user)
        {           
            int userId = await userRepository.UpdateUser(mapper.Map<Users>(user), id);
            return Ok(userId);
        }

        [HttpPut("updatingPassword/{id}")]
        public async Task<IActionResult> UpdateUserPassword(int id, UpdatePasswordView passwordView)
        {           
            int userId = await userRepository.UpdateAccountPassword(passwordView.Password, id);
            return Ok(userId);
        }

        private async Task<bool> IsUserExist(string login)
        {
            AddViewUsers user = mapper.Map<AddViewUsers>(await userRepository.GetUserByLogin(login));            
            return (user != null) ;
        }
    }
}