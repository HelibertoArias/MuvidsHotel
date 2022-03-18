using AutoMapper;
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


        //CreateMap<Event, EventListVm>().ReverseMap();
        //CreateMap<Event, CreateEventCommand>().ReverseMap();
        //CreateMap<Event, UpdateEventCommand>().ReverseMap();
        //CreateMap<Event, EventDetailVm>().ReverseMap();
        //CreateMap<Event, CategoryEventDto>().ReverseMap();
        //CreateMap<Event, EventExportDto>().ReverseMap();

        //CreateMap<Category, CategoryDto>();
        //CreateMap<Category, CategoryListVm>();
        //CreateMap<Category, CategoryEventListVm>();
        //CreateMap<Category, CreateCategoryCommand>();
        //CreateMap<Category, CreateCategoryDto>();

        //CreateMap<Order, OrdersForMonthDto>();
    }
}

