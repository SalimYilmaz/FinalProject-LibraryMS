using Application.Features.Auth.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RegisterEmployee;

public class EmployeeRegisterValidator : AbstractValidator<EmployeeRegisterCommand>
{
    public EmployeeRegisterValidator()
    {
        RuleFor(e=>e.EmployeeForRegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(e=>e.EmployeeForRegisterDto.Password).
            NotEmpty().
            MinimumLength(8).
            Must(strongPassword).WithMessage
            ("\"Password must contain at least one uppercase letter, one lowercase letter, one number and one special character.\"");
        RuleFor(e => e.EmployeeForRegisterDto.RegisterForDto.Surname).NotEmpty();
        RuleFor(e => e.EmployeeForRegisterDto.RegisterForDto.Name).NotEmpty();
        RuleFor(e => e.EmployeeForRegisterDto.RegisterForDto.Address).NotEmpty().MinimumLength(4).WithMessage("Enter a correct address.");
        RuleFor(e => e.EmployeeForRegisterDto.RegisterForDto.PhoneNumber).NotEmpty().Length(10).WithMessage("Phone number must be 10 digits long.");
        RuleFor(e=>e.EmployeeForRegisterDto.RegisterForDto.LibraryId).NotEmpty();
        RuleFor(e => e.EmployeeForRegisterDto.RegisterForDto.NationalIdentity).NotEmpty().Must(nationalIdentity => nationalIdentity.Length.Equals(11));
        RuleFor(e=>e.EmployeeForRegisterDto.RegisterForDto.Position).NotEmpty();
    }
    private bool strongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);
        return strongPasswordRegex.IsMatch(value);
        
    }
}
