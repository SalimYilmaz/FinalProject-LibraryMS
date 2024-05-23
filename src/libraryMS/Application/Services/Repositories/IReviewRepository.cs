using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IReviewRepository : IAsyncRepository<Review, Guid>, IRepository<Review, Guid>
{
}