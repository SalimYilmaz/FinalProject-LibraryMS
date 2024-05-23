using FluentValidation;

namespace Application.Features.LibraryMembers.Commands.Update;

public class UpdateLibraryMemberCommandValidator : AbstractValidator<UpdateLibraryMemberCommand>
{
    public UpdateLibraryMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.LibraryId).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
    }
}