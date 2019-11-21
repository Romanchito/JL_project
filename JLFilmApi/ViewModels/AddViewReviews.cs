using System;

namespace JLFilmApi.ViewModels
{
    public class AddViewReviews
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int FilmId { get; set; }
    }
}
