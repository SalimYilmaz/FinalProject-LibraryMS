using NArchitecture.Core.Application.Responses;

namespace Application.Features.LibraryMembers.Commands.Delete;

public class DeletedLibraryMemberResponse : IResponse
{
    public Guid Id { get; set; }
}