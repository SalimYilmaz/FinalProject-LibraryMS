using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ItemAuthors;

public interface IItemAuthorService
{
    Task<ItemAuthor?> GetAsync(
        Expression<Func<ItemAuthor, bool>> predicate,
        Func<IQueryable<ItemAuthor>, IIncludableQueryable<ItemAuthor, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ItemAuthor>?> GetListAsync(
        Expression<Func<ItemAuthor, bool>>? predicate = null,
        Func<IQueryable<ItemAuthor>, IOrderedQueryable<ItemAuthor>>? orderBy = null,
        Func<IQueryable<ItemAuthor>, IIncludableQueryable<ItemAuthor, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ItemAuthor> AddAsync(ItemAuthor itemAuthor);
    Task<ItemAuthor> UpdateAsync(ItemAuthor itemAuthor);
    Task<ItemAuthor> DeleteAsync(ItemAuthor itemAuthor, bool permanent = false);
}
