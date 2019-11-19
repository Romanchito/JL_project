using JLFilmApi.Context;
using JLFilmApi.Models;
using JLFilmApi.Repo.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<int?> DeleteUser(int? userId)
        {

            Users user = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                using (var transaction = jLDatabaseContext.Database.BeginTransaction())
                {
                    try
                    {
                        var usersComments = jLDatabaseContext.Comments.Where(x => x.UserId == user.Id);
                        jLDatabaseContext.Comments.RemoveRange(usersComments);
                        jLDatabaseContext.Users.Remove(user);
                        await jLDatabaseContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return userId;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await jLDatabaseContext.Users.ToListAsync();
        }

        public async Task<Users> GetUserById(int? userId)
        {
            Users user = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }

        public async Task UpdateUser(Users user)
        {
            jLDatabaseContext.Update(user);
            await jLDatabaseContext.SaveChangesAsync();
        }
    }
}
