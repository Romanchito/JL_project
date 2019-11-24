using AutoMapper;
using JLFilmApi.DomainModels;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            if (type != "Film" && type!="User")
            {
                return BadRequest("Wrong type");
            }
            string imagePath = await resourcePathResolver.Take(new TakingImageModel(type,fileName));
            if (imagePath == null)
            {
                return null;
            }
            return File(imagePath, "image/png");
        }

        [Authorize]
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
            int userId = (await userRepository.GetUserByLogin(User.Identity.Name)).Id;
            await userRepository.UpdateAccountImage(imageName, userId);
        }
    }
}