using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<List<Reviews>> GetAllReviewsOfFilm(int filmId)
        {
            return await jLDatabaseContext.Reviews.Where(x => x.FilmId == filmId).ToListAsync();

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

        public async Task<Reviews> GetReviewById(int reviewId)
        {
            return await jLDatabaseContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
        }
    }
}
