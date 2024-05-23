using NArchitecture.Core.Application.Responses;

namespace Application.Features.Borrows.Commands.Update;

public class UpdatedBorrowResponse : IResponse
{
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime ReturnDate { get; set; }
}