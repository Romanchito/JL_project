using JLFilmApi.Models;
using JLFilmApi.Repo.Contracts;
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
        public async Task<List<Comments>> GetComments(int? id)
        {
            return await commentsRepository.GetAllCommentsOfReview(id);
        }
    }
}