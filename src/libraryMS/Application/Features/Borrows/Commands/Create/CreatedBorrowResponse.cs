using NArchitecture.Core.Application.Responses;

namespace Application.Features.Borrows.Commands.Create;

public class CreatedBorrowResponse : IResponse
{
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime ReturnDate { get; set; }
}