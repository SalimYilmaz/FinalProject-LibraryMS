using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ReviewRepository : EfRepositoryBase<Review, Guid, BaseDbContext>, IReviewRepository
{
    public ReviewRepository(BaseDbContext context) : base(context)
    {
    }
}