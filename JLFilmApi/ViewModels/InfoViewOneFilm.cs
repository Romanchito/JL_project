using System;
using System.Collections.Generic;

namespace JLFilmApi.ViewModels
{
    public class InfoViewOneFilm
    {        
        public string Name { get; set; }
        public string Director { get; set; }
        public string Stars { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal WorldwideGross { get; set; }
        public string FilmImage { get; set; }
        public string FilmImageUrl { get; set; }
    }
}
