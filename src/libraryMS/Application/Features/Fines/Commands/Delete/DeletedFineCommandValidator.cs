using FluentValidation;

namespace Application.Features.Fines.Commands.Delete;

public class DeleteFineCommandValidator : AbstractValidator<DeleteFineCommand>
{
    public DeleteFineCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}