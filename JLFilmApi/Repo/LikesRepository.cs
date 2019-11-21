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
    public class LikesRepository : ILikesRepository
    {
        private JLDatabaseContext jLDatabaseContext;
        private readonly IMapper likeMapper;

        public LikesRepository(JLDatabaseContext jLDatabaseContext, IMapper mapper)
        {
            this.jLDatabaseContext = jLDatabaseContext;
            likeMapper = mapper;
        }

        public async Task<List<InfoViewLikes>> GetAllLikesOfReviews(int? reviewId)
        {
            List<InfoViewLikes> list = likeMapper.Map<List<InfoViewLikes>>(await jLDatabaseContext.Likes.Where(x => x.ReviewId == reviewId).ToListAsync());
            return list;
        }

        public async Task<int> AddNewLike(InfoViewLikes like)
        {
            Likes newLike = likeMapper.Map<Likes>(like);
            await jLDatabaseContext.AddAsync(newLike);
            await jLDatabaseContext.SaveChangesAsync();
            return newLike.Id;
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
