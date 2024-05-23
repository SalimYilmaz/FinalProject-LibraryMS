using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IItemRepository : IAsyncRepository<Item, Guid>, IRepository<Item, Guid>
{

}