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
        private static IWebHostEnvironment myEnvironment;

        public ImageController(IBinaryResourcePathResolver resourcePathResolver, IWebHostEnvironment environment, IUserRepository userRepository,
                IMapper mapper)
        {
            myEnvironment = environment;
            userMapper = mapper;
            this.resourcePathResolver = resourcePathResolver;
            this.userRepository = userRepository;
        }

        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            byte[] byteArray = await resourcePathResolver.FindAndGet(imageName);
            if (byteArray == null)
            {
                return null;
            }
            return File(byteArray, "image/jpeg");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(myEnvironment.WebRootPath + @"\AccountImages\"))
                {
                    Directory.CreateDirectory(myEnvironment.WebRootPath + @"\AccountImages\");
                }

                using (FileStream fileStream = System.IO.File.Create(myEnvironment.WebRootPath + @"\AccountImages\" + file.FileName))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    await UploadDataImage(file.FileName);
                    return Ok("Image " + file.FileName + " uploaded");
                }
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