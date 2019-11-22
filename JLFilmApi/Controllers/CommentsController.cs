using AutoMapper;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
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
        private IMapper mapper;

        public CommentsController(ICommentsRepository commentsRepository, IMapper mapper)
        {
            this.commentsRepository = commentsRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<List<InfoViewComments>> GetComments(int? id)
        {
            return mapper.Map<List<InfoViewComments>>(await commentsRepository.GetAllCommentsOfReview(id));
        }

        [HttpPost("addForReview")]
        public async Task<IActionResult> AddComment(InfoViewComments comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = await commentsRepository.AddNewComment(mapper.Map<Comments>(comment));
            return Ok("Add comment");

        }
    }
}