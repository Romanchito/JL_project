using System;
using System.Collections.Generic;

namespace JLFilmApi.DomainModels
{
    public partial class Users
    {
        public Users()
        {
            Comments = new HashSet<Comments>();
            Likes = new HashSet<Likes>();
            Reviews = new HashSet<Reviews>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AccountImage { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Likes> Likes { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
