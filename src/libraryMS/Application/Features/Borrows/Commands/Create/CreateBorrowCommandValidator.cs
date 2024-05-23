using FluentValidation;

namespace Application.Features.Borrows.Commands.Create;

public class CreateBorrowCommandValidator : AbstractValidator<CreateBorrowCommand>
{
    public CreateBorrowCommandValidator()
    {
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.ItemId).NotEmpty();
        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}