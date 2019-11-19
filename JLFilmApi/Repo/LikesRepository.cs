using JLFilmApi.Context;
using JLFilmApi.Models;
using JLFilmApi.Repo.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo
{
    public class LikesRepository : ILikesRepository
    {
        private JLDatabaseContext jLDatabaseContext;

        public LikesRepository(JLDatabaseContext jLDatabaseContext)
        {
            this.jLDatabaseContext = jLDatabaseContext;
        }

        public async Task<List<Likes>> GetAllLikesOfReviews(int? reviewId)
        {
            return await jLDatabaseContext.Likes.Where(x => x.ReviewId == reviewId).ToListAsync();
        }
    }
}
