using FluentValidation;

namespace Muvids.Application.Features.Movies.Commands.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.IsPublic).NotNull();

        RuleFor(x => x.Description).NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MinimumLength(1).WithMessage("{PropertyName} must have at least one character.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.Language).NotEmpty();

        RuleFor(x => x.ReleaseYear).GreaterThan(1900);

    }
}
