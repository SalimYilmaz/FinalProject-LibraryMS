using FluentValidation;

namespace Application.Features.Locations.Commands.Create;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(c => c.Section).NotEmpty();
        RuleFor(c => c.FloorNumber).NotEmpty();
        RuleFor(c => c.ShelfNumber).NotEmpty();
    }
}