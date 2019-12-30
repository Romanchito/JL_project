using System;
using System.ComponentModel.DataAnnotations;

namespace JLFilmApi.ViewModels
{
    public class AddViewReviews
    {       
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 50, ErrorMessage = "Недопустимая длина")]
        public string Text { get; set; }
        [Required]
        public int FilmId { get; set; }
    }
}
