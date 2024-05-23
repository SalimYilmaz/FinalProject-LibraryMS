using FluentValidation;

namespace Application.Features.LibraryMembers.Commands.Create;

public class CreateLibraryMemberCommandValidator : AbstractValidator<CreateLibraryMemberCommand>
{
    public CreateLibraryMemberCommandValidator()
    {
        RuleFor(c => c.MemberId).NotEmpty();
    }
}