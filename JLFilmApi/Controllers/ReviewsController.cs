using AutoMapper;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private IUserRepository userRepository;
        private IMapper mapper;

        public ReviewsController(IReviewsRepository reviewsRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.reviewsRepository = reviewsRepository;
        }

        [HttpGet("reviewsOfFilm/{id}/{skipIndex}/{takeIndex}")]
        public async Task<AllReviewsOfFilm> GetReview(int id, int skipIndex, int takeIndex)
        {
            AllReviewsOfFilm reviewsOfFilm = new AllReviewsOfFilm();
            reviewsOfFilm.Reviews = mapper.Map<List<InfoViewReviews>>
                (await reviewsRepository.GetAllReviewsOfFilm(id, skipIndex, takeIndex));
            reviewsOfFilm.CountOfReviews = await reviewsRepository.GetCountReviewsOfFilm(id);
            return reviewsOfFilm;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InfoViewReviews>> GetReviewById(int id)
        {           
            var review = mapper.Map<InfoViewReviews>(await reviewsRepository.GetReviewById(id));            
            return review;
        }

        [Authorize]
        [HttpPost("newReview")]
        public async Task<IActionResult> AddReview(AddViewReviews review)
        {            
            int userId = (await userRepository.GetUserByLogin(User.Identity.Name)).Id;
            int reviewId = await reviewsRepository.AddReview(mapper.Map<Reviews>(review), userId);
            return Ok(reviewId);
        }

        [Authorize]
        [HttpGet("reviewsOfUser")]
        public async Task<List<InfoViewReviews>> GetReviewsOfUser()
        {
            List<InfoViewReviews> listReviews = mapper.Map<List<InfoViewReviews>>
                (await reviewsRepository.GetReviewsOfUser(User.Identity.Name));           
            return listReviews;
        }             
    }
}
