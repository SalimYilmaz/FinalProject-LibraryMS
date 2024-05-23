using NArchitecture.Core.Application.Responses;

namespace Application.Features.Borrows.Queries.GetById;

public class GetByIdBorrowResponse : IResponse
{
   /* public Guid Id { get; set; } */// Buna gerek yok //
    public Guid ItemId { get; set; } // Item bilgilerine detaylý olarak eriþebilir tuþa bastýktan sonra bu id yi verip
    public DateTime ReturnDate { get; set; }
    public string MemberName { get; set; }

}