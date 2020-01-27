using JLFilmApi.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IReviewsRepository
    {
        Task<Reviews> GetReviewById(int reviewId);
        Task<List<Reviews>> GetAllReviewsOfFilm(int filmId, int skipIndex, int takeIndex);
        Task<int> AddReview(Reviews review, int userId);
        Task<List<Reviews>> GetReviewsOfUser(string login);
        Task<List<Reviews>> GetAllReviews();
        Task<int> GetCountReviewsOfFilm(int id);
    }
}
