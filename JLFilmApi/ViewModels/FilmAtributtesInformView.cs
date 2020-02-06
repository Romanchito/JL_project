using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.ViewModels
{
    public class FilmAtributtesInformView
    {
        public FilmAtributtesInformView()
        {
            FilmTypes = new List<string>();
            FilmCountries = new List<string>();
        }

        public List<string> FilmTypes { get; set; }
        public List<string> FilmCountries { get; set; }
        
    }
}
