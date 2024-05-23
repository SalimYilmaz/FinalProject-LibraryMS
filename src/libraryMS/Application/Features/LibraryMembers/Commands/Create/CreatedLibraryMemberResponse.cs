using NArchitecture.Core.Application.Responses;

namespace Application.Features.LibraryMembers.Commands.Create;

public class CreatedLibraryMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LibraryId { get; set; }
    public Guid MemberId { get; set; }
}