using NArchitecture.Core.Application.Responses;

namespace Application.Features.Payments.Commands.Create;

public class CreatedPaymentResponse : IResponse
{
    public string CreditCartHolderName { get; set; }
    public string CreditCartHolderSurname { get; set; }
    public string CreditCardNo { get; set; }
    public string Cvc { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime PaymentDate { get; set; }
}