using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JL_project.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Stars { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal WorlwideGross { get; set; }
    }
}
