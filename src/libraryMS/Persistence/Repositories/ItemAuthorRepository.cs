using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Nest;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ItemAuthorRepository : EfRepositoryBase<ItemAuthor, Guid, BaseDbContext>, IItemAuthorRepository
{
    public ItemAuthorRepository(BaseDbContext context) : base(context)
    {
    }
}