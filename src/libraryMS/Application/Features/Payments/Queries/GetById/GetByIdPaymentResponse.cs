using NArchitecture.Core.Application.Responses;

namespace Application.Features.Payments.Queries.GetById;

public class GetByIdPaymentResponse : IResponse
{
    public string CreditCartHolderName { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string MemberTotalDebt { get; set; }
    public string CreditCardNo { get; set; }
    public string Cvc { get; set; }
}