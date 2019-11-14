using System;
using System.Collections.Generic;

namespace JLFilmApi.Models
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ReviewId { get; set; }

        public virtual Reviews Review { get; set; }
        public virtual Users User { get; set; }
    }
}
