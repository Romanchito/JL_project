using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo
{
    public class ReviewsRepository : IReviewsRepository
    {
        private JLDatabaseContext jLDatabaseContext;
        public ReviewsRepository(JLDatabaseContext jLDatabaseContext)
        {
            this.jLDatabaseContext = jLDatabaseContext;
        }

        public async Task<List<Reviews>> GetAllReviewsOfFilm(int? filmId)
        {
            return await jLDatabaseContext.Reviews.Where(x => x.FilmId == filmId).ToListAsync();
        }

        public async Task<int> AddReview(Reviews review)
        {
            await jLDatabaseContext.Reviews.AddAsync(review);
            await jLDatabaseContext.SaveChangesAsync();
            return review.Id;
        }
    }
}
