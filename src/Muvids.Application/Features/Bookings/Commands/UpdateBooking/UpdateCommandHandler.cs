using AutoMapper;
using MediatR;
using Muvids.Application.Contracts.Persistence;
using Muvids.Application.Exceptions;
using Muvids.Application.Features.Bookings.Commands.CreateBooking;
using Muvids.Domain.Entities;
using ValidationException = Muvids.Application.Exceptions.ValidationException;

namespace Muvids.Application.Features.Bookings.Commands.UpdateBooking;
public class UpdateCommandHandler : IRequestHandler<UpdateBookingCommand, UpdateBookingCommandResponse>
{
    private readonly IMapper _mappper;
    private readonly IBookingRepository _bookingRepository;

    public UpdateCommandHandler(IMapper mappper,
                                       IBookingRepository bookingRepository)
    {
        this._mappper = mappper ?? throw new ArgumentNullException(nameof(mappper));
        this._bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
    }
    public async Task<UpdateBookingCommandResponse> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingToUpdate = await _bookingRepository.GetByIdAsync(request.Id);

       

        if (bookingToUpdate == null || bookingToUpdate.IsDeleted )
        {
            throw new NotFoundException(nameof(Booking), request.Id);
        }

        var updateBookingCommandResponse = new UpdateBookingCommandResponse();
        var validator = new UpdateBookingCommandValidator(_bookingRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult);
        }

        if (updateBookingCommandResponse.Success)
        {
            var entity = _mappper.Map(request, bookingToUpdate, typeof(UpdateBookingCommand), typeof(Booking));
            var hasConflicts = await _bookingRepository.HasBookingConflictsAsync(bookingToUpdate);

            if (hasConflicts)
            {
                throw new ConflictBookingException("There conflict with this booking.");
            }

            await _bookingRepository.UpdateAsync(bookingToUpdate);

            updateBookingCommandResponse.Booking = _mappper.Map<UpdateBookingDto>(entity);
        }

        return updateBookingCommandResponse;

    }
}
