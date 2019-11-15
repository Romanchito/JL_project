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
            if (jLDatabaseContext != null)
            {
                await jLDatabaseContext.Users.AddAsync(user);
                await jLDatabaseContext.SaveChangesAsync();
                return user.Id;
            }
            return 0;
        }

        public async Task<int?> DeleteUser(int? userId)
        {
            if(jLDatabaseContext != null)
            {
                Users user = await jLDatabaseContext.Users.FirstOrDefaultAsync(x=> x.Id == userId); 
                if(user != null)
                {
                    var usersComments = jLDatabaseContext.Comments.Where(x => x.UserId == user.Id);
                    jLDatabaseContext.Comments.RemoveRange(usersComments);
                    jLDatabaseContext.Users.Remove(user);
                    await jLDatabaseContext.SaveChangesAsync();
                    return userId;
                }
            }
            return 0;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            if(jLDatabaseContext != null)
            {
                return await jLDatabaseContext.Users.ToListAsync();
            }
            return null;
        }

        public async Task<Users> GetUserById(int? userId)
        {
            if(jLDatabaseContext != null)
            {
                Users user = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if(user != null)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task UpdateUser(Users user)
        {
           if(jLDatabaseContext != null)
            {
                jLDatabaseContext.Update(user);
                await jLDatabaseContext.SaveChangesAsync();
            }
        }
    }
}
