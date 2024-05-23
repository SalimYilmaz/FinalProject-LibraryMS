using NArchitecture.Core.Application.Responses;

namespace Application.Features.LibraryMembers.Commands.Update;

public class UpdatedLibraryMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LibraryId { get; set; }
    public Guid MemberId { get; set; }
}