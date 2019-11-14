using JLFilmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUserById(int? userId);
        Task<int> AddUser(Users user);
        Task<int?> DeleteUser(int? userId);
        Task UpdateUser(Users user);
    }
}
