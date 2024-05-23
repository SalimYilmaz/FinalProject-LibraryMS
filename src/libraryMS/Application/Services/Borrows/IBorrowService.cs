using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Features.Items.Rules;
using Application.Services.Items;

namespace Application.Services.Borrows;

public interface IBorrowService
{
    Task<Borrow?> GetAsync(
        Expression<Func<Borrow, bool>> predicate,
        Func<IQueryable<Borrow>, IIncludableQueryable<Borrow, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Borrow>?> GetListAsync(
        Expression<Func<Borrow, bool>>? predicate = null,
        Func<IQueryable<Borrow>, IOrderedQueryable<Borrow>>? orderBy = null,
        Func<IQueryable<Borrow>, IIncludableQueryable<Borrow, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Borrow> AddAsync(Borrow borrow);
    Task<Borrow> UpdateAsync(Borrow borrow);
    Task<Borrow> DeleteAsync(Borrow borrow, bool permanent = false);
}
