using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
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

        public LikesController(ILikesRepository likesRepository)
        {
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

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLike(int userId, int reviewId)
        {
            int? result = await likesRepository.DeleteLike(userId, reviewId);
            if (result == 0 || result == null)
            {
                return NotFound(result);
            }
            return Ok();
        }
    }
}