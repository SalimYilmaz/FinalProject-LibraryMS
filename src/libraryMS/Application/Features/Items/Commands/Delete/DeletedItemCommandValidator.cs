using FluentValidation;

namespace Application.Features.Items.Commands.Delete;

public class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}