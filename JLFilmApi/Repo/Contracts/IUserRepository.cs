using JLFilmApi.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IUserRepository
    {
        Task<Users> GetUserById(int userId);
        Task<int> AddUser(Users user);        
        Task<int> UpdateUser(Users user, int id);
        Task<Users> GetUserByLogin(string login);
        Task<int> UpdateAccountImage(string imageName, string login);
        Task<int> UpdateAccountPassword(string password, int id);  
    }
}
