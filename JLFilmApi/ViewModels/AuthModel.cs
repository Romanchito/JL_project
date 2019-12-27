using System.ComponentModel.DataAnnotations;

namespace JLFilmApi.ViewModels
{
    public class AuthModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Неверный формат ввода логина")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
