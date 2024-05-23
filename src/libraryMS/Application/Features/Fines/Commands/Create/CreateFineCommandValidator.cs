using FluentValidation;

namespace Application.Features.Fines.Commands.Create;

public class CreateFineCommandValidator : AbstractValidator<CreateFineCommand>
{
    public CreateFineCommandValidator()
    {
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.BorrowId).NotEmpty();
        RuleFor(c => c.FineType).NotEmpty();
        RuleFor(c => c.FineType).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Description).MinimumLength(2).MaximumLength(500);
    }
}