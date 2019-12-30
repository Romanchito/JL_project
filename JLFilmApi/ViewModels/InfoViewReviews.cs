using System;
using System.Collections.Generic;
using System.Linq;

namespace JLFilmApi.ViewModels
{
    public class InfoViewReviews
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }       
        public string UserLogin { get; set; }        
        public int CountOfLikes
        {
            get
            {
                return Likes.Where(x=>x.IsLike).Count();
            }            
        }        
        public int CountOfDislikes
        {
            get
            {
                return Likes.Where(x => !x.IsLike).Count();
            }
        }
        public  ICollection<InfoViewLikes> Likes { get; set; }    
        
    }
}
