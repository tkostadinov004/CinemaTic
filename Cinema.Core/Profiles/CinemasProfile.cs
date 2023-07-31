using AutoMapper;
using Cinema.ViewModels.Cinemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Profiles
{
    public class CinemasProfile : Profile
    {
        public CinemasProfile()
        {
            CreateMap<CreateCinemaViewModel, Data.Models.Cinema>();
            CreateMap<Data.Models.Cinema, CinemaListViewModel>()
                .ForMember
                (
                    item => item.MoviesCount, opt => opt.MapFrom(src => src.Movies.Count)
                );
            //.ForMember
            //(
            //    item => item.Status, opt => opt.MapFrom(src => src.IsApproved ? "Approved" : "Pending approval")
            //)

        }
    }
}
