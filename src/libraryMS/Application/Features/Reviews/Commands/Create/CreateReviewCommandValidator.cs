using FluentValidation;

namespace Application.Features.Reviews.Commands.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.ItemId).NotEmpty();
        RuleFor(c => c.ReviewTitle).NotEmpty().MaximumLength(100);
        RuleFor(c => c.ReviewDate).NotEmpty();
        RuleFor(c => c.ReviewContent).NotEmpty().MaximumLength(500);
    }
}