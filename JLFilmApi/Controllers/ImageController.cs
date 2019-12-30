using AutoMapper;
using JLFilmApi.Helpers;
using JLFilmApi.Infostructure;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    public enum Types { Film, User }

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
        [HttpGet("accountImage")]
        public async Task<string> GetImage()
        {
            string fileName = (await userRepository.GetUserByLogin(User.Identity.Name)).AccountImage;           
            return await TakingImage(Types.User, fileName);
        }

        [HttpGet("userImage/{id}")]
        public async Task<string> GetUserImage(int id)
        {
            string fileName = (await userRepository.GetUserById(id)).AccountImage;           
            return await TakingImage(Types.User, fileName);
        }

        [HttpGet("filmImage/{id}")]
        public async Task<string> GetImage(int id)
        {
            string fileName = (await filmRepository.GetFilm(id)).FilmImage;            
            return await TakingImage(Types.Film, fileName);
        }

        [Authorize]
        [HttpPost("uploading")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var  userAccountImage = (await userRepository.GetUserByLogin(User.Identity.Name)).AccountImage;
            if(userAccountImage != null)
            {
                resourcePathResolver.DeleteUnusingImage(userAccountImage);
            }
            string imageName = await resourcePathResolver.Upload(file, User.Identity.Name);
            await UploadDataImage(imageName);
            return Ok(imageName);
        }
               
        private async Task<string> TakingImage(Types type, string fileName)
        {
            if (fileName == null) 
            {
                fileName = (type == Types.Film) ? 
                    ImageDefaultNames.DEFAULT_FILM_IMAGE_NAME : 
                    ImageDefaultNames.DEFAULT_USER_IMAGE_NAME;
            }
            string imagePath = await resourcePathResolver.Take(new TakingImageModel(type, fileName));                
            return imagePath;
        }


        private async Task UploadDataImage(string imageName)
        {
            await userRepository.UpdateAccountImage(imageName, User.Identity.Name);
           
        }
    }
}