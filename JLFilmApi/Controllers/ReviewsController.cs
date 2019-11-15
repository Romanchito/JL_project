using JLFilmApi.Models;
using JLFilmApi.Repo.Contracts;
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
        
        [HttpGet("{id}")]
        public async Task<List<Reviews>> Get(int? id)
        {
            return await reviewsRepository.GetAllReviewsOfFilm(id);           
           
        }       
    }
}
