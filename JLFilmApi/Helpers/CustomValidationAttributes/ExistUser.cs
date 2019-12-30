using JLFilmApi.Context;
using JLFilmApi.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Helpers.CustomValidationAttributes
{
    public class ExistUser : ValidationAttribute
    {
        private JLDatabaseContext context;

        public ExistUser(JLDatabaseContext context)
        {
            this.context = context;
        }
        public override bool IsValid(object value)
        {
            AuthModel user = value as AuthModel;

            using (context)
            {
                var existingUser = context.Users.FirstOrDefault(x => x.Login == user.Username && x.Password == user.Password);
                if(existingUser == null)
                {
                    this.ErrorMessage = "Такого пользователя не существует";
                    return false;
                }
            }
            return true;
        }
    }
}
