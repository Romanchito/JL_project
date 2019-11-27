using System.ComponentModel.DataAnnotations;

namespace JLFilmApi.ViewModels
{
    public class AuthModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
