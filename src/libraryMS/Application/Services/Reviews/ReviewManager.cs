using Application.Features.Reviews.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Reviews;

public class ReviewManager : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ReviewBusinessRules _reviewBusinessRules;

    public ReviewManager(IReviewRepository reviewRepository, ReviewBusinessRules reviewBusinessRules)
    {
        _reviewRepository = reviewRepository;
        _reviewBusinessRules = reviewBusinessRules;
    }

    public async Task<Review?> GetAsync(
        Expression<Func<Review, bool>> predicate,
        Func<IQueryable<Review>, IIncludableQueryable<Review, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Review? review = await _reviewRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return review;
    }

    public async Task<IPaginate<Review>?> GetListAsync(
        Expression<Func<Review, bool>>? predicate = null,
        Func<IQueryable<Review>, IOrderedQueryable<Review>>? orderBy = null,
        Func<IQueryable<Review>, IIncludableQueryable<Review, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Review> reviewList = await _reviewRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return reviewList;
    }

    public async Task<Review> AddAsync(Review review)
    {
        Review addedReview = await _reviewRepository.AddAsync(review);

        return addedReview;
    }

    public async Task<Review> UpdateAsync(Review review)
    {
        Review updatedReview = await _reviewRepository.UpdateAsync(review);

        return updatedReview;
    }

    public async Task<Review> DeleteAsync(Review review, bool permanent = false)
    {
        Review deletedReview = await _reviewRepository.DeleteAsync(review);

        return deletedReview;
    }
}
