using AutoMapper;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private IMapper mapper;

        public LikesController(ILikesRepository likesRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.likesRepository = likesRepository;
        }

        [HttpGet("allOfReview/{id}")]
        public async Task<List<InfoViewLikes>> GetLikes(int id)
        {
            if (await likesRepository.GetAllLikesOfReviews(id) == null)
            {
                throw new NullReferenceException("Review with this id not found");
            }

            return mapper.Map<List<InfoViewLikes>>(await likesRepository.GetAllLikesOfReviews(id));
        }

        [HttpPost("newLike")]
        public async Task<IActionResult> AddLike(InfoViewLikes like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int likeId = await likesRepository.AddNewLike(mapper.Map<Likes>(like));
            return Ok(likeId);
        }
       
    }
}