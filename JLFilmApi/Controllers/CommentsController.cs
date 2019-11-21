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
        
        public CommentsController(ICommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        [HttpGet("{id}")]
        public async Task<List<InfoViewComments>> GetComments(int? id)
        {
            return await commentsRepository.GetAllCommentsOfReview(id);
        }

        [HttpPost("addForReview")]
        public async Task<IActionResult> AddComment(InfoViewComments comment)
        {
            if (ModelState.IsValid)
            {
                int id = await commentsRepository.AddNewComment(comment);
                if(id > 0)
                {
                    return Ok("Add comment");
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