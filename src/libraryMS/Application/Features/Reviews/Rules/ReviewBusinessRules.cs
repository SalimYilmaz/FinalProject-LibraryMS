using Application.Features.Reviews.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Reviews.Rules;

public class ReviewBusinessRules : BaseBusinessRules
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ILocalizationService _localizationService;

    public ReviewBusinessRules(IReviewRepository reviewRepository, ILocalizationService localizationService)
    {
        _reviewRepository = reviewRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ReviewsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ReviewShouldExistWhenSelected(Review? review)
    {
        if (review == null)
            await throwBusinessException(ReviewsBusinessMessages.ReviewNotExists);
    }

    public async Task ReviewIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Review? review = await _reviewRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ReviewShouldExistWhenSelected(review);
    }
}