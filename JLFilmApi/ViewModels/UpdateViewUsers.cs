using System.ComponentModel.DataAnnotations;

namespace JLFilmApi.ViewModels
{
    public class UpdateViewUsers 
    {      
        [Required]
        [RegularExpression(@"[A-Za-z]*", ErrorMessage = "Некорректное имя")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string Name { get; set; }
        [RegularExpression(@"[A-Za-z]*", ErrorMessage = "Некорректное имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина фамилии")]
        public string Surname { get; set; }        
    }
}
