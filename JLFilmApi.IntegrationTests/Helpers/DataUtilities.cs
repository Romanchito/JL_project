﻿using JLFilmApi.Context;
using JLFilmApi.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace JLFilmApi.IntegrationTests.Helpers
{
    public class DataUtilities
    {
        public static void ReInitializeDbForTests(JLDatabaseContext db)
        {
            db.Users.RemoveRange(db.Users);
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
                new Users(){ Login = "user1" , Password = "1234", Name = "Roman", Surname = "Tretyakov" },
                new Users(){ Login = "user2" , Password = "1111", Name = "Test", Surname = "Test" },
            };
        }
    }
}
