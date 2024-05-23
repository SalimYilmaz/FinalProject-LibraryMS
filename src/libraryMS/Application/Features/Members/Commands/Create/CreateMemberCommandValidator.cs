using FluentValidation;

namespace Application.Features.Members.Commands.Create;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(30).Must(name => char.IsUpper(name[0])).WithMessage("First letter of the name must be uppercase.");
        RuleFor(c => c.Surname).NotEmpty().MinimumLength(2).MaximumLength(30).Must(surname => char.IsUpper(surname[0])).WithMessage("First letter of the name must be uppercase.");
        RuleFor(c => c.PhoneNumber).NotEmpty().Length(10).WithMessage("Phone number must be 10 digits long.");
        RuleFor(c => c.Address).NotEmpty().MinimumLength(4).WithMessage("Enter a correct address.");
        RuleFor(c => c.UserId).NotEmpty();
    }
}