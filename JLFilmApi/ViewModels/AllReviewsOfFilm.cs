using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.ViewModels
{
    public class AllReviewsOfFilm
    {
        public List<InfoViewReviews> Reviews { get; set; }
        public int CountOfReviews { get; set; }        
    }
}
