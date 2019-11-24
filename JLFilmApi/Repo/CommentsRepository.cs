using AutoMapper;
using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo
{
    public class CommentsRepository : ICommentsRepository
    {
        private JLDatabaseContext jLDatabaseContext;        

        public CommentsRepository(JLDatabaseContext jLDatabaseContext)
        {
            this.jLDatabaseContext = jLDatabaseContext;            
        }

        public async Task<List<Comments>> GetAllCommentsOfReview(int? reviewId)
        {
            return await jLDatabaseContext.Comments.Where(x => x.ReviewId == reviewId).ToListAsync();            
        }

        public async Task<int> AddNewComment(Comments comment)
        {           
            await jLDatabaseContext.Comments.AddAsync(comment);
            await jLDatabaseContext.SaveChangesAsync();
            return comment.Id;
        }
    }
}
