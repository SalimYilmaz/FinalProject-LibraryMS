using FluentValidation;

namespace Application.Features.Borrows.Commands.Update;

public class UpdateBorrowCommandValidator : AbstractValidator<UpdateBorrowCommand>
{
    public UpdateBorrowCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.ItemId).NotEmpty();
        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}