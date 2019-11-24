using System.Collections.Generic;

namespace JLFilmApi.ViewModels
{
    public class InfoViewUsers
    {        
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AccountImage { get; set; }
        public virtual ICollection<InfoViewReviews> Reviews { get; set; }
    }
}
