using NArchitecture.Core.Application.Responses;

namespace Application.Features.Payments.Commands.Update;

public class UpdatedPaymentResponse : IResponse
{
    public string PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentType { get; set; }
    public bool IsPaid { get; set; }
}