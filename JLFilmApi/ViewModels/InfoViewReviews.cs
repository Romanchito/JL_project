using System;
using System.Collections.Generic;

namespace JLFilmApi.ViewModels
{
    public class InfoViewReviews
    {        
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int FilmId { get; set; }

        public virtual ICollection<InfoViewComments> Comments { get; set; }
        public virtual ICollection<InfoViewLikes> Likes { get; set; }
    }
}
