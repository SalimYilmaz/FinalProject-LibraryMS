using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILibraryMemberRepository : IAsyncRepository<LibraryMember, Guid>, IRepository<LibraryMember, Guid>
{
}