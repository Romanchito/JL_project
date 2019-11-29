using JLFilmApi.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface ICommentsRepository
    {
        Task<List<Comments>> GetAllCommentsOfReview(int reviewId);
        Task<int> AddNewComment(Comments comment, int userId);
    }
}
