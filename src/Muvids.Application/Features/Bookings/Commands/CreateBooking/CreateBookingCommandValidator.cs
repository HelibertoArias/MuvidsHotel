using FluentValidation;
using Muvids.Application.Contracts.Persistence;
using Muvids.Application.Features.Bookings.Commands.UpdateBooking;
using Muvids.Application.Features.Bookings.Query.AvailableBookings;

namespace Muvids.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    private readonly IBookingRepository bookingRepository;

    public CreateBookingCommandValidator(IBookingRepository bookingRepository)
    {

        this.bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));

        // TODO: Move this to the appsettings.json file.
        int minDayInAdvance = 1;

        int maxBookLenghtDays = 3;
        var minDateInAdvance = DateTime.Now.Date.AddDays(minDayInAdvance);

        int maxDayInAdvance = 30;


        RuleFor(x => x.Start)
           .NotNull()
           .WithMessage("{PropertyName} is required");

        RuleFor(x => x.End)
            .NotNull()
            .WithMessage("{PropertyName} is required");

        RuleFor(x => x.Start)
            .LessThan(x => x.End)
            .WithMessage("Start date should be before the end date.");

        RuleFor(x => x.Start)
            .GreaterThanOrEqualTo(minDateInAdvance)
            .WithMessage($"Start date should greater than or equals to {minDateInAdvance.Date.ToShortDateString()}");

        RuleFor(x => x.End)
            .Must((x, end) => (end - x.Start).TotalDays < maxDayInAdvance)
            .WithMessage($"You can not reserve with more than {maxDayInAdvance} days in advance.");

        RuleFor(x => x.End)
            .Must((x, end) => (end - x.Start).TotalDays < maxBookLenghtDays)
            .WithMessage($"You can not reserve for more than {maxBookLenghtDays} days lenght.");

        RuleFor(x => x.Start)
           .NotNull()
           .WithMessage("{PropertyName} is required");
    }

}
public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    private readonly IBookingRepository bookingRepository;

    public UpdateBookingCommandValidator(IBookingRepository bookingRepository)
    {
        this.bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));

        // TODO: Move this to the appsettings.json file.
        int minDayInAdvance = 1;

        int maxBookLenghtDays = 3;
        var minDateInAdvance = DateTime.Now.Date.AddDays(minDayInAdvance);

        int maxDayInAdvance = 30;


        RuleFor(x => x.Start)
           .NotNull()
           .WithMessage("{PropertyName} is required");

        RuleFor(x => x.End)
            .NotNull()
            .WithMessage("{PropertyName} is required");

        RuleFor(x => x.Start)
            .LessThan(x => x.End)
            .WithMessage("Start date should be before the end date.");

        RuleFor(x => x.Start)
            .GreaterThanOrEqualTo(minDateInAdvance)
            .WithMessage($"Start date should greater than or equals to {minDateInAdvance.Date.ToShortDateString()}");

        RuleFor(x => x.End)
            .Must((x, end) => (end - x.Start).TotalDays < maxDayInAdvance)
            .WithMessage($"You can not reserve with more than {maxDayInAdvance} days in advance.");

        RuleFor(x => x.End)
            .Must((x, end) => (end - x.Start).TotalDays < maxBookLenghtDays)
            .WithMessage($"You can not reserve for more than {maxBookLenghtDays} days lenght.");

        RuleFor(x => x.Start)
           .NotNull()
           .WithMessage("{PropertyName} is required");
    }

}
public class CheckRoomQueryValidator : AbstractValidator<CheckRoomAvailabilityQuery>
{
    private readonly IBookingRepository bookingRepository;

    public CheckRoomQueryValidator(IBookingRepository bookingRepository)
    {
        this.bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));

        // TODO: Move this to the appsettings.json file.
        int minDayInAdvance = 1;

        int maxBookLenghtDays = 3;
        var minDateInAdvance = DateTime.Now.Date.AddDays(minDayInAdvance);

        int maxDayInAdvance = 30;


        RuleFor(x => x.Start)
           .NotNull()
           .WithMessage("{PropertyName} is required");

        RuleFor(x => x.End)
            .NotNull()
            .WithMessage("{PropertyName} is required");

        RuleFor(x => x.Start)
            .LessThan(x => x.End)
            .WithMessage("Start date should be before the end date.");

        RuleFor(x => x.Start)
            .GreaterThanOrEqualTo(minDateInAdvance)
            .WithMessage($"Start date should greater than or equals to {minDateInAdvance.Date.ToShortDateString()}");

        RuleFor(x => x.End)
            .Must((x, end) => (end - x.Start).TotalDays < maxDayInAdvance)
            .WithMessage($"You can not reserve with more than {maxDayInAdvance} days in advance.");

        RuleFor(x => x.End)
            .Must((x, end) => (end - x.Start).TotalDays < maxBookLenghtDays)
            .WithMessage($"You can not reserve for more than {maxBookLenghtDays} days lenght.");

        RuleFor(x => x.Start)
           .NotNull()
           .WithMessage("{PropertyName} is required");
    }

}
