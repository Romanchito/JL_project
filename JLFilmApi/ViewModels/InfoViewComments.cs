using System;

namespace JLFilmApi.ViewModels
{
    public class InfoViewComments
    {      
        public int Id { get; set; }
        public string Text { get; set; }                
        public int ReviewId { get; set; }
        public int UserId { get; set; }
    }
}
