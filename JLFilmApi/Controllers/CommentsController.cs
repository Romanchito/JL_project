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
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private ICommentsRepository commentsRepository;
        private IUserRepository userRepository;
        private IMapper mapper;

        public CommentsController(ICommentsRepository commentsRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.commentsRepository = commentsRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("review/{id}")]
        public async Task<List<InfoViewComments>> GetComments(int id)
        {
            return mapper.Map<List<InfoViewComments>>(await commentsRepository.GetAllCommentsOfReview(id));
        }

        [Authorize]
        [HttpPost("newComment")]
        public async Task<IActionResult> AddComment(InfoViewComments comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = (await userRepository.GetUserByLogin(User.Identity.Name)).Id;
            int commentId = await commentsRepository.AddNewComment(mapper.Map<Comments>(comment),id);
            return Ok(commentId);

        }
    }
}