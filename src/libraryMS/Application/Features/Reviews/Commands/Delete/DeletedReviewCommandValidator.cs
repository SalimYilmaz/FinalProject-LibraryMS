using FluentValidation;

namespace Application.Features.Reviews.Commands.Delete;

public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}