using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class LikesController : ControllerBase
    {
        private ILikesRepository likesRepository;
        private IUserRepository userRepository;

        public LikesController(ILikesRepository likesRepository, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.likesRepository = likesRepository;
        }

        [HttpGet("allOfReview{id}")]
        public async Task<List<InfoViewLikes>> GetLikes(int? id)
        {
            return await likesRepository.GetAllLikesOfReviews(id);
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddLike(InfoViewLikes like)
        {
            like.UserId = (await userRepository.GetUserByLogin(User.Identity.Name)).Id;
            if (ModelState.IsValid)
            {
                int id = await likesRepository.AddNewLike(like);
                if (id > 0)
                {
                    return Ok("Add Like");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLike(int reviewId)
        {
            int userId = (await userRepository.GetUserByLogin(User.Identity.Name)).Id;
            int? result = await likesRepository.DeleteLike(userId, reviewId);
            if (result == 0 || result == null)
            {
                return NotFound(result);
            }
            return Ok(User.Identity.Name+ "delete's like from review" + reviewId.ToString());
        }
    }
}