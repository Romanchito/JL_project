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
            List<InfoViewReviews> listReviews = await getCountOfLikesToReviews(id);
            await getUserNameToReviews(listReviews, id);
            return listReviews;
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


        private async Task<List<InfoViewReviews>> getCountOfLikesToReviews(int id)
        {
            int index = 0;
            var dataList = await reviewsRepository.GetAllReviewsOfFilm(id);
            List<InfoViewReviews> resultList = mapper.Map<List<InfoViewReviews>>
                (await reviewsRepository.GetAllReviewsOfFilm(id));

            foreach (var item in dataList)
            {
                resultList.ElementAt(index).LikesCount = item.Likes.Count;
                index++;
            }

            return resultList;
        }

        private async Task getUserNameToReviews(List<InfoViewReviews> listReviews, int id)
        {
            int index = 0;
            var dataList = await reviewsRepository.GetAllReviewsOfFilm(id);
            foreach (var item in dataList)
            {
                listReviews.ElementAt(index).UserLogin = (await userRepository.GetUserById(item.UserId)).Login;
                index++;
            }
        }
    }
}
