using JLFilmApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IReviewsRepository
    {
        Task<List<InfoViewReviews>> GetAllReviewsOfFilm(int? filmId);
        Task<int> AddReview(AddViewReviews review);
    }
}
