using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.ViewModels
{
    public class UpdatePasswordView
    {
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Недопустимая длина пароля(6-20)")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage="Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
