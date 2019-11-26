using JLFilmApi.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface ILikesRepository
    {
        Task<List<Likes>> GetAllLikesOfReviews(int reviewId);
        Task<int> AddNewLike(Likes like);
        Task<int> DeleteLike(int userId, int reviewId);        
    }
}
