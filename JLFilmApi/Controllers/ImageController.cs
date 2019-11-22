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

        [HttpGet("Get/{type}/{fileName}")]
        public async Task<IActionResult> GetImage(string type, string fileName)
        {
            string imagePath = await resourcePathResolver.Take(new TakingImageModel(type,fileName));
            if (imagePath == null)
            {
                return null;
            }
            return File(imagePath, "image/jpeg");
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
            user.AccountImage = imageName;
            UpdateViewUsers updateUser = userMapper.Map<UpdateViewUsers>(user);
            await userRepository.UpdateUser(updateUser, updateUser.Id);
        }
    }
}