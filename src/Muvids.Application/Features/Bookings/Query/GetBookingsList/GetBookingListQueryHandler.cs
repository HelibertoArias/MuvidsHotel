using AutoMapper;
using MediatR;
using Muvids.Application.Contracts;
using Muvids.Application.Contracts.Persistence;

namespace Muvids.Application.Features.Bookings.Query.GetBookingsList;
public class GetBookingListQueryHandler : IRequestHandler<GetBookingListQuery, GetBookingListQueryResponse>
{

    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;
    private readonly ILoggedInUserService _loggedInUserService;

    public GetBookingListQueryHandler(IMapper mappper,
                                       IBookingRepository bookingRepository,
                                       ILoggedInUserService loggedInUserService)
    {
        this._mapper = mappper ?? throw new ArgumentNullException(nameof(mappper));
        this._bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        this._loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
    }


    public async Task<GetBookingListQueryResponse> Handle(GetBookingListQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _bookingRepository.ListAllByUserAsync(_loggedInUserService.UserId);

        var resultdtos = _mapper.Map<List<BookingDto>>(bookings);

        var result = new GetBookingListQueryResponse
        {
            Bookings = resultdtos
        };

        return result;
    }
}
