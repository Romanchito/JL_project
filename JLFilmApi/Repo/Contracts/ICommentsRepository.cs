using JLFilmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface ICommentsRepository
    {
        Task<List<Comments>> GetAllCommentsOfReview(int? reviewId);
    }
}
