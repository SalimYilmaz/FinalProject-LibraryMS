using Application.Features.LibraryMembers.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LibraryMembers;

public class LibraryMemberManager : ILibraryMemberService
{
    private readonly ILibraryMemberRepository _libraryMemberRepository;
    private readonly LibraryMemberBusinessRules _libraryMemberBusinessRules;

    public LibraryMemberManager(ILibraryMemberRepository libraryMemberRepository, LibraryMemberBusinessRules libraryMemberBusinessRules)
    {
        _libraryMemberRepository = libraryMemberRepository;
        _libraryMemberBusinessRules = libraryMemberBusinessRules;
    }

    public async Task<LibraryMember?> GetAsync(
        Expression<Func<LibraryMember, bool>> predicate,
        Func<IQueryable<LibraryMember>, IIncludableQueryable<LibraryMember, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LibraryMember? libraryMember = await _libraryMemberRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return libraryMember;
    }

    public async Task<IPaginate<LibraryMember>?> GetListAsync(
        Expression<Func<LibraryMember, bool>>? predicate = null,
        Func<IQueryable<LibraryMember>, IOrderedQueryable<LibraryMember>>? orderBy = null,
        Func<IQueryable<LibraryMember>, IIncludableQueryable<LibraryMember, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LibraryMember> libraryMemberList = await _libraryMemberRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return libraryMemberList;
    }

    public async Task<LibraryMember> AddAsync(LibraryMember libraryMember)
    {
        LibraryMember addedLibraryMember = await _libraryMemberRepository.AddAsync(libraryMember);

        return addedLibraryMember;
    }

    public async Task<LibraryMember> UpdateAsync(LibraryMember libraryMember)
    {
        LibraryMember updatedLibraryMember = await _libraryMemberRepository.UpdateAsync(libraryMember);

        return updatedLibraryMember;
    }

    public async Task<LibraryMember> DeleteAsync(LibraryMember libraryMember, bool permanent = false)
    {
        LibraryMember deletedLibraryMember = await _libraryMemberRepository.DeleteAsync(libraryMember);

        return deletedLibraryMember;
    }
}
