using NArchitecture.Core.Application.Responses;
using Org.BouncyCastle.Asn1.Mozilla;

namespace Application.Features.Borrows.Commands.Delete;

public class DeletedBorrowResponse : IResponse
{
    public Guid Id { get; set; }
}