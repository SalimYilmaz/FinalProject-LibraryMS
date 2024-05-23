using NArchitecture.Core.Application.Responses;

namespace Application.Features.LibraryMembers.Queries.GetById;

public class GetByIdLibraryMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LibraryId { get; set; }
    public Guid MemberId { get; set; }
}