using AutoMapper;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IBinaryResourcePathResolver resourcePathResolver;        
        private IUserRepository userRepository;
        private IFilmRepository filmRepository;

        public ImageController(IBinaryResourcePathResolver resourcePathResolver, IUserRepository userRepository,
                               IFilmRepository filmRepository)
        {            
            this.resourcePathResolver = resourcePathResolver;
            this.userRepository = userRepository;
            this.filmRepository = filmRepository;
        }


        [Authorize]
        [HttpGet("userImage")]
        public async Task<string> GetImage()
        {
            string fileName = (await userRepository.GetUserByLogin(User.Identity.Name)).AccountImage;
            if (fileName == null) fileName = "default_user.png";
            return await TakingImage("user", fileName);
        }

        [HttpGet("filmImage/{id}")]
        public async Task<string> GetImage(int id)
        {
            string fileName = (await filmRepository.GetFilm(id)).FilmImage;
            if (fileName == null) fileName = "default_film.png";
            return await TakingImage("film", fileName);
        }

        [Authorize]
        [HttpPost("uploading")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            string userLogin = (await userRepository.GetUserByLogin(User.Identity.Name)).Login;
            string imageName = await resourcePathResolver.Upload(file, userLogin);

            if (imageName != null)
            {
                return Ok();
            }
            return BadRequest();
        }
               
        private async Task<string> TakingImage(string type, string fileName)
        {
            string imagePath = await resourcePathResolver.Take(new TakingImageModel(type, fileName));
            if (imagePath == null)
            {
                return null;
            }            
            return imagePath;
        }


    }
}