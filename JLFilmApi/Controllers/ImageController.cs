using AutoMapper;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Authorization;
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
        private IFilmRepository filmRepository;

        public ImageController(IBinaryResourcePathResolver resourcePathResolver, IUserRepository userRepository,
                               IMapper mapper, IFilmRepository filmRepository)
        {
            userMapper = mapper;
            this.resourcePathResolver = resourcePathResolver;
            this.userRepository = userRepository;
            this.filmRepository = filmRepository;
        }


        [Authorize]
        [HttpGet("GetUserImage")]
        public async Task<IActionResult> GetImage()
        {
            string fileName = (await userRepository.GetUserByLogin(User.Identity.Name)).AccountImage;
            return await TakingImage("user", fileName);
        }

        [HttpGet("GetFilmImage/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            string fileName = (await filmRepository.GetFilm(id)).FilmImage;
            return await TakingImage("film", fileName);
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            string imageName = await resourcePathResolver.Upload(file);
            if (imageName != null)
            {
                await UploadDataImage(imageName);
                return Ok("image " + imageName + " is added");
            }
            return BadRequest();
        }

        private async Task UploadDataImage(string imageName)
        {
            int userId = (await userRepository.GetUserByLogin(User.Identity.Name)).Id;
            string oldAccountImage = (await userRepository.GetUserByLogin(User.Identity.Name)).AccountImage;
            await userRepository.UpdateAccountImage(imageName, userId);
            if (oldAccountImage != null)
            {
                DeleteUnusingImage(oldAccountImage);
            }
        }

        private async Task<IActionResult> TakingImage(string type, string fileName)
        {
            string imagePath = await resourcePathResolver.Take(new TakingImageModel(type, fileName));
            if (imagePath == null)
            {
                return null;
            }
            return File(imagePath, "image/png");
        }

        private void DeleteUnusingImage(string oldAccountImage)
        {
            System.IO.File.Delete($"wwwroot/{Path.Combine("AccountImages", oldAccountImage)}");
        }
    }
}