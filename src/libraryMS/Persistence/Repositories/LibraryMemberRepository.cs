using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LibraryMemberRepository : EfRepositoryBase<LibraryMember, Guid, BaseDbContext>, ILibraryMemberRepository
{
    public LibraryMemberRepository(BaseDbContext context) : base(context)
    {
    }
}