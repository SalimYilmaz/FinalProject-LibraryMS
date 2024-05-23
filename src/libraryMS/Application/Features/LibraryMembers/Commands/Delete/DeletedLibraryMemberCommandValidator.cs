using FluentValidation;

namespace Application.Features.LibraryMembers.Commands.Delete;

public class DeleteLibraryMemberCommandValidator : AbstractValidator<DeleteLibraryMemberCommand>
{
    public DeleteLibraryMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}