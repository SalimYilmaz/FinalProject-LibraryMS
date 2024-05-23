using FluentValidation;

namespace Application.Features.Publishers.Commands.Update;

public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(30).Must(name => char.IsUpper(name[0])).WithMessage("First letter of the name must be uppercase.");  // Bunu setter içerisinde ctor ile yapadabiliriz.
        RuleFor(c => c.PhoneNumber).NotEmpty().Must(phone => phone.Length == 7 || phone.Length == 10 || phone.Length == 11);
    }
}