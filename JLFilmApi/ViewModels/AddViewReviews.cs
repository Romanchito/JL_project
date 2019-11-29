using System;
using System.ComponentModel.DataAnnotations;

namespace JLFilmApi.ViewModels
{
    public class AddViewReviews
    {       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }        
        public int FilmId { get; set; }
    }
}
