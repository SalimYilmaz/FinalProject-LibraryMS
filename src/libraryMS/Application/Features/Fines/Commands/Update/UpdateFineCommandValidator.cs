using FluentValidation;

namespace Application.Features.Fines.Commands.Update;

public class UpdateFineCommandValidator : AbstractValidator<UpdateFineCommand>
{
    public UpdateFineCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.BorrowId).NotEmpty();
        RuleFor(c => c.FineAmount).NotEmpty();
        RuleFor(c => c.FineType).NotEmpty();
        RuleFor(c => c.IssueDate).NotEmpty();
        RuleFor(c => c.Description).MinimumLength(2).MaximumLength(500);
        RuleFor(c => c.Paid).NotEmpty();
    }
}