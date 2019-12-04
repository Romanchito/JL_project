using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JLFilmApi.ViewModels
{
    public class InfoViewFilms
    {       
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }     
        public string FilmImage { get; set; }
        public string FilmImageUrl { get; set; }
    }
}
