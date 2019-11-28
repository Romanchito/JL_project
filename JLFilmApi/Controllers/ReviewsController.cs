using AutoMapper;
using JLFilmApi.DomainModels;
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
        private IMapper mapper;

        public ReviewsController(IReviewsRepository reviewsRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.reviewsRepository = reviewsRepository;
        }

        [HttpGet("reviewsOfFilm/{id}")]
        public async Task<List<InfoViewReviews>> GetReview(int id)
        {
            return mapper.Map<List<InfoViewReviews>>(await reviewsRepository.GetAllReviewsOfFilm(id));
        }

        [HttpPost("newReview")]
        public async Task<IActionResult> AddReview(AddViewReviews review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int reviewId = await reviewsRepository.AddReview(mapper.Map<Reviews>(review));
            return Ok(reviewId);
        }
    }
}
