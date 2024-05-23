using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NArchitecture.Core.Persistence.Repositories;
using Nest;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ItemRepository : EfRepositoryBase<Item, Guid, BaseDbContext>, IItemRepository
{
    public ItemRepository(BaseDbContext context) : base(context)
    {
    }
}