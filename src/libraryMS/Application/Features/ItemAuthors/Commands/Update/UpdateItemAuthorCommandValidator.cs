using FluentValidation;

namespace Application.Features.ItemAuthors.Commands.Update;

public class UpdateItemAuthorCommandValidator : AbstractValidator<UpdateItemAuthorCommand>
{
    public UpdateItemAuthorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ItemId).NotEmpty();
        RuleFor(c => c.AuthorId).NotEmpty();
    }
}