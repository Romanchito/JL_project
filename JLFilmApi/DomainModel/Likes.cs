using System;
using System.Collections.Generic;

namespace JLFilmApi.DomainModels
{
    public class Likes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public bool IsLike { get; set; }
        public virtual Reviews Review { get; set; }
        public virtual Users User { get; set; }
    }
}
