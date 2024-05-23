using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Features.Items.Rules;

namespace Application.Services.Items;

public interface IItemService
{
    Task<Item?> GetAsync(
        Expression<Func<Item, bool>> predicate,
        Func<IQueryable<Item>, IIncludableQueryable<Item, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Item>?> GetListAsync(
        Expression<Func<Item, bool>>? predicate = null,
        Func<IQueryable<Item>, IOrderedQueryable<Item>>? orderBy = null,
        Func<IQueryable<Item>, IIncludableQueryable<Item, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Item> AddAsync(Item item);
    Task<Item> UpdateAsync(Item item);
    Task<Item> DeleteAsync(Item item, bool permanent = false);

    public Task setCurrentItemInStock(Guid id, bool stockUpdateDirection);
}
