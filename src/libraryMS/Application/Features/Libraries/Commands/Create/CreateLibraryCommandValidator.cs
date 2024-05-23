using FluentValidation;

namespace Application.Features.Libraries.Commands.Create;

public class CreateLibraryCommandValidator : AbstractValidator<CreateLibraryCommand>
{
    public CreateLibraryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(60).Must(name => char.IsUpper(name[0])).WithMessage("First letter of the name must be uppercase.");  // Bunu setter içerisinde ctor ile yapadabiliriz.
        RuleFor(c => c.Address).NotEmpty().MinimumLength(4).WithMessage("Enter a correct address.");
        RuleFor(c => c.PhoneNumber).NotEmpty().Must(phone => phone.Length == 7 || phone.Length == 10 || phone.Length == 11);
        RuleFor(c => c.City).NotEmpty().MinimumLength(2).MaximumLength(30);
        RuleFor(c => c.Website).NotEmpty();
    }
}