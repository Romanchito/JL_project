using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLFilmApi.Models
{
    public class Film
    {       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Stars { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string WorldwideGross { get; set; }

    }
}
