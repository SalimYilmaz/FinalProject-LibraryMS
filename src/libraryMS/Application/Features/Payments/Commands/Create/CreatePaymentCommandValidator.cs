using FluentValidation;

namespace Application.Features.Payments.Commands.Create;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(c => c.CreditCartHolderName).NotEmpty();
        RuleFor(c => c.CreditCartHolderSurname).NotEmpty();
        RuleFor(c => c.Cvc).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.ExpiryDate).NotEmpty();

    }
}