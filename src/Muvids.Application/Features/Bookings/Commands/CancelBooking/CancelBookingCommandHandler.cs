using AutoMapper;
using MediatR;
using Muvids.Application.Contracts;
using Muvids.Application.Contracts.Persistence;
using Muvids.Application.Exceptions;
using Muvids.Domain.Entities;

namespace Muvids.Application.Features.Bookings.Commands.CancelBooking;
public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;
    private readonly ILoggedInUserService _loggedInUserService;

    public CancelBookingCommandHandler(IMapper mappper,
                                       IBookingRepository bookingRepository,
                                       ILoggedInUserService loggedInUserService)
    {
        this._mapper = mappper ?? throw new ArgumentNullException(nameof(mappper));
        this._bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        this._loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
    }


    public async Task<Unit> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingToUpdate = await _bookingRepository.GetByIdAsync(request.Id);
        if (bookingToUpdate == null)
        {
            throw new NotFoundException(nameof(Booking), request.Id);
        }

        if (bookingToUpdate.CreatedBy != _loggedInUserService.UserId)
        {
            throw new BadRequestException("You can not update this booking. It belongs to other user.");
        }

        bookingToUpdate.IsDeleted = true;
        await _bookingRepository.UpdateAsync(bookingToUpdate);

        return Unit.Value;
    }
}
