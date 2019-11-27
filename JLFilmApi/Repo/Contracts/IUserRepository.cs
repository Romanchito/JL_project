using JLFilmApi.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IUserRepository
    {
        Task<Users> GetUserById(int userId);
        Task<int> AddUser(Users user);
        Task<int?> DeleteUser(int userId);
        Task UpdateUser(Users user, int id);
        Task<Users> GetUserByLogin(string login);
        Task UpdateAccountImage(string imageName, int id);
    }
}
