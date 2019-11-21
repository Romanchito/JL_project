using AutoMapper;
using JLFilmApi.DomainModels;
using JLFilmApi.ViewModels;

namespace JLFilmApi.Infostructure
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AddViewUsers, Users>().ReverseMap(); ;                           // map from AddViewUsers to Users
            CreateMap<UpdateViewUsers, Users>().ReverseMap(); ;                        // map from UpdateViewUsers to Users
            CreateMap<InfoViewUsers, Users>().ReverseMap(); ;                          // map from Users to InfoViewUsers
            CreateMap<Reviews, InfoViewReviews>().ReverseMap(); ;                      // map from Reviews to InfoViewReviews
            CreateMap<Comments, InfoViewComments>().ReverseMap(); ;                    // map from Comments to InfoViewComments
            CreateMap<Likes, InfoViewLikes>().ReverseMap(); ;                          // map from Likes to InfoViewLikes
            CreateMap<Films, InfoViewFilms>().ReverseMap();                            // map from Films to InfoViewFilms
            CreateMap<Reviews, AddViewReviews>().ReverseMap();                         // map from Reviews to AddViewReviews
            CreateMap<Films, InfoViewOneFilm>().ReverseMap();                            // map from Films to InfoViewOneFilm
        }
    }
}
