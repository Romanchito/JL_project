using JLFilmApi.Context;
using JLFilmApi.DomainModels;
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
            if (
                    await jLDatabaseContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId) == null
               )
            {
                return null;
            }
            return await jLDatabaseContext.Likes.Where(x => x.ReviewId == reviewId).ToListAsync();
        }

        public async Task<int> AddNewLike(Likes like)
        {
            await jLDatabaseContext.AddAsync(like);
            await jLDatabaseContext.SaveChangesAsync();
            return like.Id;
        }

        public async Task<int> DeleteLike(int userId, int reviewId)
        {
            Likes deleteLike = await jLDatabaseContext.Likes.FirstOrDefaultAsync(x => x.ReviewId == reviewId && x.UserId == userId);
            jLDatabaseContext.Likes.Remove(deleteLike);
            await jLDatabaseContext.SaveChangesAsync();
            return reviewId;
        }
    }
}
