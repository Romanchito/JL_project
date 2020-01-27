using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<Reviews>> GetAllReviewsOfFilm(int filmId, int skipIndex, int takeIndex)
        {
            //skipIndex decreases by 1 because in the database the index starts at 0
            skipIndex--;            
            return await jLDatabaseContext.Reviews.Where(x => x.FilmId == filmId)
                .Skip(skipIndex * takeIndex)
                .Take(takeIndex)
                .ToListAsync();
        }
       
        public async Task<int> AddReview(Reviews review, int userId)
        {            
            review.UserId = userId;
            review.Date = DateTime.Now;
            await jLDatabaseContext.Reviews.AddAsync(review);
            await jLDatabaseContext.SaveChangesAsync();
            return review.Id;
        }

        public async Task<List<Reviews>> GetReviewsOfUser(string login)
        {
            int userId = (await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Login == login)).Id;
            var reviewList =await jLDatabaseContext.Reviews.Where(r => r.UserId == userId).ToListAsync();
            return reviewList;
        }

        public async Task<List<Reviews>> GetAllReviews()
        {
            return await jLDatabaseContext.Reviews.ToListAsync();            
        }

        public async Task<int> GetCountReviewsOfFilm(int id)
        {
            return await jLDatabaseContext.Reviews.Where(x => x.FilmId == id).CountAsync();
        }

        public async Task<Reviews> GetReviewById(int reviewId)
        {
            return await jLDatabaseContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
        }
    }
}
