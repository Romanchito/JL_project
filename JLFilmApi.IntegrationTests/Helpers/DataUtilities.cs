using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JLFilmApi.IntegrationTests.Helpers
{
    public class DataUtilities
    {
        public static void ReInitializeDbForTests(JLDatabaseContext db)
        {
            db.Comments.RemoveRange(db.Comments);
            db.Likes.RemoveRange(db.Likes);
            db.Reviews.RemoveRange(db.Reviews);
            db.Users.RemoveRange(db.Users);
            db.Films.RemoveRange(db.Films);            
            InitializeDbForTests(db);
        }

        private static void InitializeDbForTests(JLDatabaseContext db)
        {
            db.Users.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        private static List<Users> GetSeedingMessages()
        {
            return new List<Users>
            {
                new Users(){ Login = "user1" , Password = "1234", Name = "Roman",
                    Surname = "Tretyakov", AccountImage = "tarantino.jpg" },
                new Users(){ Login = "user2" , Password = "1111", Name = "Test", Surname = "Test" },
            };
        }        
    }
}
