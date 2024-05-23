using System.Text.RegularExpressions;
using Application.Features.Auth.Dtos;
using FluentValidation;

namespace Application.Features.Auth.Commands.RegisterMember;

public class MemberRegisterCommandValidator : AbstractValidator<MemberRegisterCommand>
{
    public MemberRegisterCommandValidator()
    {
        RuleFor(m => m.MemberForRegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(m => m.MemberForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Must(strongPassword)
            .WithMessage(
                "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character."
            );
        RuleFor(m=>m.MemberForRegisterDto.RegisterForDto.Surname).NotEmpty();
        RuleFor(m=>m.MemberForRegisterDto.RegisterForDto.Name).NotEmpty();
        RuleFor(m=>m.MemberForRegisterDto.RegisterForDto.Address).NotEmpty();
        RuleFor(m=>m.MemberForRegisterDto.RegisterForDto.PhoneNumber).NotEmpty();
        RuleFor(m => m.MemberForRegisterDto.RegisterForDto.NationalIdentity).NotEmpty().Must(nationalIdentity=> nationalIdentity.Length.Equals(11));
    }

    private bool strongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
