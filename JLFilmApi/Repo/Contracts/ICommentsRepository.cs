using JLFilmApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface ICommentsRepository
    {
        Task<List<InfoViewComments>> GetAllCommentsOfReview(int? reviewId);
        Task<int> AddNewComment(InfoViewComments comment);
    }
}
