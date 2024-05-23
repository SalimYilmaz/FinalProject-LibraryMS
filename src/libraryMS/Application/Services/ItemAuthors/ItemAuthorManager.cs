using Application.Features.ItemAuthors.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ItemAuthors;

public class ItemAuthorManager : IItemAuthorService
{
    private readonly IItemAuthorRepository _itemAuthorRepository;
    private readonly ItemAuthorBusinessRules _itemAuthorBusinessRules;

    public ItemAuthorManager(IItemAuthorRepository itemAuthorRepository, ItemAuthorBusinessRules itemAuthorBusinessRules)
    {
        _itemAuthorRepository = itemAuthorRepository;
        _itemAuthorBusinessRules = itemAuthorBusinessRules;
    }

    public async Task<ItemAuthor?> GetAsync(
        Expression<Func<ItemAuthor, bool>> predicate,
        Func<IQueryable<ItemAuthor>, IIncludableQueryable<ItemAuthor, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ItemAuthor? itemAuthor = await _itemAuthorRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return itemAuthor;
    }

    public async Task<IPaginate<ItemAuthor>?> GetListAsync(
        Expression<Func<ItemAuthor, bool>>? predicate = null,
        Func<IQueryable<ItemAuthor>, IOrderedQueryable<ItemAuthor>>? orderBy = null,
        Func<IQueryable<ItemAuthor>, IIncludableQueryable<ItemAuthor, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ItemAuthor> itemAuthorList = await _itemAuthorRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return itemAuthorList;
    }

    public async Task<ItemAuthor> AddAsync(ItemAuthor itemAuthor)
    {
        ItemAuthor addedItemAuthor = await _itemAuthorRepository.AddAsync(itemAuthor);

        return addedItemAuthor;
    }

    public async Task<ItemAuthor> UpdateAsync(ItemAuthor itemAuthor)
    {
        ItemAuthor updatedItemAuthor = await _itemAuthorRepository.UpdateAsync(itemAuthor);

        return updatedItemAuthor;
    }

    public async Task<ItemAuthor> DeleteAsync(ItemAuthor itemAuthor, bool permanent = false)
    {
        ItemAuthor deletedItemAuthor = await _itemAuthorRepository.DeleteAsync(itemAuthor);

        return deletedItemAuthor;
    }
}
