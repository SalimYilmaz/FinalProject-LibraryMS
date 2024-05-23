using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LibraryMembers;

public interface ILibraryMemberService
{
    Task<LibraryMember?> GetAsync(
        Expression<Func<LibraryMember, bool>> predicate,
        Func<IQueryable<LibraryMember>, IIncludableQueryable<LibraryMember, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LibraryMember>?> GetListAsync(
        Expression<Func<LibraryMember, bool>>? predicate = null,
        Func<IQueryable<LibraryMember>, IOrderedQueryable<LibraryMember>>? orderBy = null,
        Func<IQueryable<LibraryMember>, IIncludableQueryable<LibraryMember, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LibraryMember> AddAsync(LibraryMember libraryMember);
    Task<LibraryMember> UpdateAsync(LibraryMember libraryMember);
    Task<LibraryMember> DeleteAsync(LibraryMember libraryMember, bool permanent = false);
}
