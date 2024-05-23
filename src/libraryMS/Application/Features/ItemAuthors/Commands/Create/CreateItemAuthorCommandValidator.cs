using FluentValidation;

namespace Application.Features.ItemAuthors.Commands.Create;

public class CreateItemAuthorCommandValidator : AbstractValidator<CreateItemAuthorCommand>
{
    public CreateItemAuthorCommandValidator()
    {
        RuleFor(c => c.AuthorId).NotEmpty();
    }
}