using JLFilmApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLFilmApi.Repo.Contracts
{
    public interface IUserRepository
    {
        Task<List<InfoViewUsers>> GetAllUsers();
        Task<InfoViewUsers> GetUserById(int? userId);
        Task<int> AddUser(AddViewUsers user);
        Task<int?> DeleteUser(int? userId);
        Task UpdateUser(UpdateViewUsers user, int id);
        Task<InfoViewUsers> GetUserByLogin(string login);
    }
}
