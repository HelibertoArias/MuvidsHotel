using AutoMapper;
using MediatR;
using Muvids.Application.Contracts.Persistence;
using Muvids.Application.Exceptions;
using Muvids.Domain.Entities;

namespace Muvids.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;

    public CreateBookingCommandHandler(IMapper mappper,
                                       IBookingRepository bookingRepository)
    {
        this._mapper = mappper ?? throw new ArgumentNullException(nameof(mappper));
        this._bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
    }

    public async Task<CreateBookingCommandResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var createBookingCommandResponse = new CreateBookingCommandResponse();
        var validator = new CreateBookingCommandValidator(_bookingRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult);
        }

        if (createBookingCommandResponse.Success)
        {
         
            var entity = _mapper.Map<Booking>(request);

            var hasConflicts = await _bookingRepository.HasBookingConflictsAsync(entity);
            if (hasConflicts)
            {
                throw new ConflictBookingException("There conflict with this booking.");
            }

            entity = await _bookingRepository.AddAsync(entity);
            createBookingCommandResponse.Booking = _mapper.Map<CreateBookingDto>(entity);
        }

        return createBookingCommandResponse;
       
    }
}
