using JLFilmApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface ILikesRepository
    {
        Task<List<InfoViewLikes>> GetAllLikesOfReviews(int? reviewId);
        Task<int> AddNewLike(InfoViewLikes like);
        Task<int> DeleteLike(int userId, int reviewId);
    }
}
