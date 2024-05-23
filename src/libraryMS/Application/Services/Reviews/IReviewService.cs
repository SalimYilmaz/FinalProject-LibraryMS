using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Reviews;

public interface IReviewService
{
    Task<Review?> GetAsync(
        Expression<Func<Review, bool>> predicate,
        Func<IQueryable<Review>, IIncludableQueryable<Review, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Review>?> GetListAsync(
        Expression<Func<Review, bool>>? predicate = null,
        Func<IQueryable<Review>, IOrderedQueryable<Review>>? orderBy = null,
        Func<IQueryable<Review>, IIncludableQueryable<Review, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Review> AddAsync(Review review);
    Task<Review> UpdateAsync(Review review);
    Task<Review> DeleteAsync(Review review, bool permanent = false);
}
