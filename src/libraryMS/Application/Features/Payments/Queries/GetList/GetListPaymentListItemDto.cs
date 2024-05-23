using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Payments.Queries.GetList;

public class GetListPaymentListItemDto : IDto
{
    public Guid Id { get; set; }
    public string MemberName { get; set; }
    public string MemberSurname { get; set; }
    public DateTime CreatedDate { get; set; }
}