using NArchitecture.Core.Application.Dtos;

namespace Application.Features.LibraryMembers.Queries.GetList;

public class GetListLibraryMemberListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid LibraryId { get; set; }
    public Guid MemberId { get; set; }
}