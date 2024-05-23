using NArchitecture.Core.Application.Responses;

namespace Application.Features.Fines.Queries.GetById;

public class GetByIdFineResponse : IResponse
{
    public string MemberName { get; set; }
    public string MemberSurname { get; set; }
    public string Description { get; set; }
    public bool IsPaid { get; set; }
    public string FineType { get; set; }
    public DateTime BorrowReturnDate { get; set; }
}