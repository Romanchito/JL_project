using AutoMapper;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("reviewsOfFilm/{id}")]
        public async Task<List<InfoViewReviews>> GetReview(int id)
        {
            List<InfoViewReviews> listReviews = mapper.Map<List<InfoViewReviews>>
                (await reviewsRepository.GetAllReviewsOfFilm(id));
            await getViewFields(listReviews);
            return listReviews;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<InfoViewReviews>> GetReviewById(int id)
        {
            var data = await reviewsRepository.GetReviewById(id);
            var review = mapper.Map<InfoViewReviews>(data);
            await getFields(review, data);
            return review;
        }

        [Authorize]
        [HttpPost("newReview")]
        public async Task<IActionResult> AddReview(AddViewReviews review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
            await getViewFields(listReviews);
            return listReviews;
        }      

        private async Task getViewFields(List<InfoViewReviews> listReviews)
        {
            var list = await reviewsRepository.GetAllReviews();

            foreach (var item in listReviews)
            {
                //Take userLogin from list
                int userId = list.FirstOrDefault(x => x.Id == item.Id).UserId;
                item.UserLogin = (await userRepository.GetUserById(userId)).Login;

                //Take count of dislikes from list
                item.CountOdDislikes = list.FirstOrDefault(x => x.Id == item.Id).
                                     Likes.Where(like => like.IsLike == false)
                                     .Count();
                //Take count of likes from list
                item.CountOfLikes = list.FirstOrDefault(x => x.Id == item.Id).
                                       Likes.Where(like => like.IsLike == true)
                                       .Count();

            }
        }

        private async Task getFields(InfoViewReviews item, Reviews review)
        {
            
            int userId = review.UserId;
            item.UserLogin = (await userRepository.GetUserById(userId)).Login;

            //Take count of dislikes from review
            item.CountOdDislikes = review.Likes.Where(like => like.IsLike == false)
                                 .Count();
            //Take count of likes from review
            item.CountOfLikes = review.Likes.Where(like => like.IsLike == true)
                                   .Count();
        }
    }
}
