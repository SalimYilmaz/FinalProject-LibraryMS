using FluentValidation;

namespace Application.Features.Employees.Commands.Create;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(30).Must(name => char.IsUpper(name[0])).WithMessage("First letter of the name must be uppercase.");  // Bunu setter içerisinde ctor ile yapadabiliriz.
        RuleFor(c => c.Surname).NotEmpty().MinimumLength(2).MaximumLength(30).Must(surname => char.IsUpper(surname[0])).WithMessage("First letter of the name must be uppercase.");  // Bunu setter içerisinde ctor ile yapadabiliriz.
        RuleFor(c => c.NationalId).NotEmpty().Length(11).WithMessage("National ID must be 11 characters long.");
        RuleFor(c => c.PhoneNumber).NotEmpty().Length(10).WithMessage("Phone number must be 10 digits long.");
        RuleFor(c => c.Address).NotEmpty().MinimumLength(4).WithMessage("Enter a correct address.");
        RuleFor(c => c.Email).NotEmpty().EmailAddress().WithMessage("Invalid email format.");
        RuleFor(c => c.Position).NotEmpty().MinimumLength(2).MaximumLength(30);
        RuleFor(c => c.LibraryId).NotEmpty();
    }
}