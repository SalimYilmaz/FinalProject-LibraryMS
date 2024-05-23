using FluentValidation;

namespace Application.Features.Payments.Commands.Update;

public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CreditCardNo).NotEmpty();
        RuleFor(c => c.CreditCartHolderName).NotEmpty();
        RuleFor(c => c.CreditCartHolderSurname).NotEmpty();
        RuleFor(c => c.Cvc).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
    }
}