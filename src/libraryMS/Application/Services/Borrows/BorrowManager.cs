using Application.Features.Borrows.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Borrows;

public class BorrowManager : IBorrowService
{
    private readonly IBorrowRepository _borrowRepository;
    private readonly BorrowBusinessRules _borrowBusinessRules;

    public BorrowManager(IBorrowRepository borrowRepository, BorrowBusinessRules borrowBusinessRules)
    {
        _borrowRepository = borrowRepository;
        _borrowBusinessRules = borrowBusinessRules;
    }

    public async Task<Borrow?> GetAsync(
        Expression<Func<Borrow, bool>> predicate,
        Func<IQueryable<Borrow>, IIncludableQueryable<Borrow, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Borrow? borrow = await _borrowRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return borrow;
    }

    public async Task<IPaginate<Borrow>?> GetListAsync(
        Expression<Func<Borrow, bool>>? predicate = null,
        Func<IQueryable<Borrow>, IOrderedQueryable<Borrow>>? orderBy = null,
        Func<IQueryable<Borrow>, IIncludableQueryable<Borrow, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Borrow> borrowList = await _borrowRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return borrowList;
    }

    public async Task<Borrow> AddAsync(Borrow borrow)
    {
        Borrow addedBorrow = await _borrowRepository.AddAsync(borrow);

        return addedBorrow;
    }

    public async Task<Borrow> UpdateAsync(Borrow borrow)
    {
        Borrow updatedBorrow = await _borrowRepository.UpdateAsync(borrow);

        return updatedBorrow;
    }

    public async Task<Borrow> DeleteAsync(Borrow borrow, bool permanent = false)
    {
        Borrow deletedBorrow = await _borrowRepository.DeleteAsync(borrow);

        return deletedBorrow;
    }


}
