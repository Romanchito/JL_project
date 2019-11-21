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
    public class ReviewsRepository : IReviewsRepository
    {
        private JLDatabaseContext jLDatabaseContext;
        private readonly IMapper reviewMapper;
        public ReviewsRepository(JLDatabaseContext jLDatabaseContext, IMapper mapper)
        {
            reviewMapper = mapper;
            this.jLDatabaseContext = jLDatabaseContext;
        }

        public async Task<List<InfoViewReviews>> GetAllReviewsOfFilm(int? filmId)
        {
            List<InfoViewReviews> list = reviewMapper.Map<List<InfoViewReviews>>
                (await jLDatabaseContext.Reviews.Where(x => x.FilmId == filmId).ToListAsync());
            return list;
        }

        public async Task<int> AddReview(AddViewReviews review)
        {
            var newReview = reviewMapper.Map<Reviews>(review);
            await jLDatabaseContext.Reviews.AddAsync(newReview);
            await jLDatabaseContext.SaveChangesAsync();
            return newReview.Id;
        }
    }
}
