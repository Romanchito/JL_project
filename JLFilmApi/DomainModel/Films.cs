using System;
using System.Collections.Generic;

namespace JLFilmApi.DomainModels
{
    public partial class Films
    {
        public Films()
        {
            Reviews = new HashSet<Reviews>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Stars { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal WorldwideGross { get; set; }
        public string FilmImage { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
