using AutoMapper;
using MediatR;
using Muvids.Application.Contracts.Persistence;
using Muvids.Application.Exceptions;
using Muvids.Application.Features.Bookings.Commands.CreateBooking;
using Muvids.Domain.Entities;

namespace Muvids.Application.Features.Bookings.Query.AvailableBookings;
public class CheckRoomAvailabilityQueryHandler : IRequestHandler<CheckRoomAvailabilityQuery, CheckRoomAvailabilityQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;

    public CheckRoomAvailabilityQueryHandler(IMapper mappper,
                                            IBookingRepository bookingRepository)
    {
        _mapper = mappper ?? throw new ArgumentNullException(nameof(mappper));
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
    }
    public async Task<CheckRoomAvailabilityQueryResponse> Handle(CheckRoomAvailabilityQuery request, CancellationToken cancellationToken)
    {
        var checkRoomAvailabilityQueryResponse = new CheckRoomAvailabilityQueryResponse();
        var validator = new CheckRoomQueryValidator(_bookingRepository);

        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult);
        }

        var booking = _mapper.Map<Booking>(request);

        var IsAvailable = await _bookingRepository.HasBookingConflictsAsync(booking);


        checkRoomAvailabilityQueryResponse.Booking = new AvailableBookingDto
        {
            End = request.End,
            Start = request.Start,
            IsAvailable = IsAvailable
        };

        return checkRoomAvailabilityQueryResponse;
    }
}
