using System;
using System.Collections.Generic;

namespace JLFilmApi.ViewModels
{
    public class InfoViewReviews
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }       
        public string UserLogin { get; set; }
        public int LikesCount { get; set; }
    }
}
