using AutoMapper;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IBinaryResourcePathResolver resourcePathResolver;
        private IMapper userMapper;
        private IUserRepository userRepository;       

        public ImageController(IBinaryResourcePathResolver resourcePathResolver, IUserRepository userRepository, IMapper mapper)
        {           
            userMapper = mapper;
            this.resourcePathResolver = resourcePathResolver;
            this.userRepository = userRepository;
        }

        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            byte[] byteArray = await resourcePathResolver.Take(imageName);
            if (byteArray == null)
            {
                return null;
            }
            return File(byteArray, "image/jpeg");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            string imageName = await resourcePathResolver.Upload(file);
            if(imageName != null)
            {
                await UploadDataImage(imageName);
                return Ok("image " + imageName + " is added");
            }
            return BadRequest();
        }

        private async Task UploadDataImage(string imageName)
        {
            InfoViewUsers user = await userRepository.GetUserByLogin(User.Identity.Name);
            UpdateViewUsers updateUser = userMapper.Map<UpdateViewUsers>(user);
            await userRepository.UpdateUser(updateUser, updateUser.Id);
        }
    }
}