using JLFilmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface ILikesRepository
    {
        Task<List<Likes>> GetAllLikesOfReviews(int? reviewId);
    }
}
