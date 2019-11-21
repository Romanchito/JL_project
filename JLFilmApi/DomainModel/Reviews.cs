using System;
using System.Collections.Generic;

namespace JLFilmApi.DomainModels
{
    public partial class Reviews
    {
        public Reviews()
        {
            Comments = new HashSet<Comments>();
            Likes = new HashSet<Likes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int FilmId { get; set; }

        public virtual Films Film { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Likes> Likes { get; set; }
    }
}
