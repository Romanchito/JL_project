using AutoMapper;
using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo
{
    public class CommentsRepository : ICommentsRepository
    {
        private JLDatabaseContext jLDatabaseContext;
        private readonly IMapper commentsMapper;

        public CommentsRepository(JLDatabaseContext jLDatabaseContext, IMapper mapper)
        {
            this.jLDatabaseContext = jLDatabaseContext;
            commentsMapper = mapper;
        }

        public async Task<List<InfoViewComments>> GetAllCommentsOfReview(int? reviewId)
        {
            List<InfoViewComments> list = commentsMapper.Map<List<InfoViewComments>>
                (await jLDatabaseContext.Comments.Where(x => x.ReviewId == reviewId).ToListAsync());
            return list;
        }

        public async Task<int> AddNewComment(InfoViewComments comment)
        {
            Comments newComment = commentsMapper.Map<Comments>(comment);
            await jLDatabaseContext.Comments.AddAsync(newComment);
            await jLDatabaseContext.SaveChangesAsync();
            return newComment.Id;
        }
    }
}
