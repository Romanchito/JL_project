using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IReviewsRepository reviewsRepository;

        public ReviewsController(IReviewsRepository reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        [HttpGet("allOfFilm/{id}")]
        public async Task<List<InfoViewReviews>> GetReview(int? id)
        {
            return await reviewsRepository.GetAllReviewsOfFilm(id);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReview(AddViewReviews review)
        {
            if (ModelState.IsValid)
            {
                int reviewId = await reviewsRepository.AddReview(review);
                if (reviewId > 0)
                {
                    return Ok("Add Review");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest(ModelState);
        }
    }
}
