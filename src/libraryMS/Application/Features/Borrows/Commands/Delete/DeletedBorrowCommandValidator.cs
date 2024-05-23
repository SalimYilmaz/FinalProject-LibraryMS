using FluentValidation;

namespace Application.Features.Borrows.Commands.Delete;

public class DeleteBorrowCommandValidator : AbstractValidator<DeleteBorrowCommand>
{
    public DeleteBorrowCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}