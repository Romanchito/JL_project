using JLFilmApi.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IReviewsRepository
    {
        Task<List<Reviews>> GetAllReviewsOfFilm(int filmId);
        Task<int> AddReview(Reviews review, int userId);
        Task<List<Reviews>> GetReviewsOfUser(string login);
        Task<List<Reviews>> GetAllReviews();
    }
}
