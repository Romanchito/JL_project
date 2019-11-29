using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Repo
{
    public class UsersRepository : IUserRepository
    {
        private JLDatabaseContext jLDatabaseContext;

        public UsersRepository(JLDatabaseContext jLDatabaseContext)
        {
            this.jLDatabaseContext = jLDatabaseContext;
        }

        public async Task<int> AddUser(Users user)
        {
            await jLDatabaseContext.Users.AddAsync(user);
            await jLDatabaseContext.SaveChangesAsync();
            return user.Id;
        }      

        public async Task<Users> GetUserById(int userId)
        {
            return await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<int> UpdateUser(Users user, int id)
        {
            Users updateUser = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == id);           
            updateUser.Name = user.Name;
            updateUser.Surname = user.Surname;
            updateUser.Password = user.Password;           
            await jLDatabaseContext.SaveChangesAsync();
            return updateUser.Id;
        }

        public async Task<Users> GetUserByLogin(string login)
        {
            Users user = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Login == login);
            return user;
        }

        public async Task<int> UpdateAccountImage(string imageName, int id)
        {
            Users updateUser = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == id);            
            updateUser.AccountImage = imageName;           
            await jLDatabaseContext.SaveChangesAsync();
            return updateUser.Id;
        }
    }
}
