using System.ComponentModel.DataAnnotations;

namespace JLFilmApi.ViewModels
{
    public class AddViewUsers
    {        
        [Required]
        [EmailAddress(ErrorMessage ="Неверный формат почтового адреса")]
        public string Login { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Недопустимая длина пароля(6-20)")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z]*", ErrorMessage = "Некорректное имя")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z]*", ErrorMessage = "Некорректное имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина фамилии")]
        public string Surname { get; set; }        
    }
}
