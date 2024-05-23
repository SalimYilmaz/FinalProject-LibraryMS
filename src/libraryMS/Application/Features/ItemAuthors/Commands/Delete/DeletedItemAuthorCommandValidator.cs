using FluentValidation;

namespace Application.Features.ItemAuthors.Commands.Delete;

public class DeleteItemAuthorCommandValidator : AbstractValidator<DeleteItemAuthorCommand>
{
    public DeleteItemAuthorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}