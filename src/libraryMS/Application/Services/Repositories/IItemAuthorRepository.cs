using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IItemAuthorRepository : IAsyncRepository<ItemAuthor, Guid>, IRepository<ItemAuthor, Guid>
{
}