using AutoMapper;
using Muvids.Application.Features.Bookings.Commands.CreateBooking;
using Muvids.Application.Features.Movies.Commands.CreateMovie;
using Muvids.Application.Features.Movies.Commands.UpdateMovie;
using Muvids.Application.Features.Movies.Queries.GetMoviesList;
using Muvids.Domain.Entities;

namespace Muvids.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Movie, MovieListVm>().ReverseMap();

        CreateMap<Movie, CreateMovieDto>();
        CreateMap<CreateMovieCommand, Movie>();

        CreateMap<UpdateMovieCommand, Movie>();

        CreateMap<CreateBookingCommand,Booking>();
        CreateMap<Booking, CreateBookingDto>();
   
        //CreateMap<Booking, UpdteBookingDto>();
    }
}

