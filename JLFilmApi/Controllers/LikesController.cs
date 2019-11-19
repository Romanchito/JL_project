using JLFilmApi.Models;
using JLFilmApi.Repo.Contracts;
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

        [HttpGet("{id}")]
        public async Task<List<Likes>> GetLikes(int? id)
        {
            return await likesRepository.GetAllLikesOfReviews(id);
        }
    }
}