using AutoMapper;
using Muvids.Application.Features.Bookings.Commands.CreateBooking;
using Muvids.Application.Features.Bookings.Commands.UpdateBooking;
using Muvids.Application.Features.Bookings.Query.AvailableBookings;
using Muvids.Application.Features.Bookings.Query.GetBookingsList;
using Muvids.Domain.Entities;

namespace Muvids.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
       

        CreateMap<CreateBookingCommand, Booking>();
        CreateMap<Booking, CreateBookingDto>();

        CreateMap<Booking, UpdateBookingDto>();
        CreateMap<UpdateBookingCommand, Booking>();

        CreateMap<CheckRoomAvailabilityQuery, Booking>();

        CreateMap<Booking, BookingDto>();
    }
}

