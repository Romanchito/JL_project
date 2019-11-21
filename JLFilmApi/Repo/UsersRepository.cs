using AutoMapper;
using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
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
        private readonly IMapper userMapper;

        public UsersRepository(JLDatabaseContext jLDatabaseContext, IMapper mapper)
        {
            this.jLDatabaseContext = jLDatabaseContext;
            userMapper = mapper;
        }

        public async Task<int> AddUser(AddViewUsers user)
        {
            var newUser = userMapper.Map<Users>(user);
            await jLDatabaseContext.Users.AddAsync(newUser);
            await jLDatabaseContext.SaveChangesAsync();
            return newUser.Id;
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

        public async Task<List<InfoViewUsers>> GetAllUsers()
        {
            List<InfoViewUsers> list = userMapper.Map<List<InfoViewUsers>>
                (await jLDatabaseContext.Users.ToListAsync());
            return list;
        }

        public async Task<InfoViewUsers> GetUserById(int? userId)
        {
            InfoViewUsers user = userMapper.Map<InfoViewUsers>
                (await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == userId));
            return user;
        }

        public async Task UpdateUser(UpdateViewUsers user, int id)
        {
            DomainModels.Users updateUser = await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            DomainModels.Users sourseUsers = userMapper.Map<DomainModels.Users>(user);
            updateUser.Password = sourseUsers.Password;
            updateUser.Name = sourseUsers.Name;
            updateUser.Surname = sourseUsers.Surname;
            jLDatabaseContext.Update(updateUser);
            await jLDatabaseContext.SaveChangesAsync();
        }

        public async Task<InfoViewUsers> GetUserByLoginAndPassword(string login, string password)
        {
            InfoViewUsers user = userMapper.Map<InfoViewUsers>
                (await jLDatabaseContext.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password));
            return user;
        }
    }
}
