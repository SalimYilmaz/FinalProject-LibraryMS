using FluentValidation;

namespace Application.Features.Members.Commands.Update;

public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(m => m.Id).NotEmpty();
        RuleFor(m => m.Name).NotEmpty().MinimumLength(2).MaximumLength(30).Must(name => char.IsUpper(name[0])).WithMessage("First letter of the name must be uppercase.");
        RuleFor(m => m.Surname).NotEmpty().MinimumLength(2).MaximumLength(30).Must(surname => char.IsUpper(surname[0])).WithMessage("First letter of the name must be uppercase.");
        RuleFor(m => m.PhoneNumber).NotEmpty().Length(10).WithMessage("Phone number must be 10 digits long.");
        RuleFor(m => m.Address).NotEmpty().MinimumLength(4).WithMessage("Enter a correct address.");
        RuleFor(m => m.UserId).NotEmpty();
        RuleFor(m => m.TotalDebt).NotEmpty();
    }
}