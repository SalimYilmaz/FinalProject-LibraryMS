using Application.Features.Reviews.Constants;
using Application.Features.Reviews.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Reviews.Constants.ReviewsOperationClaims;

namespace Application.Features.Reviews.Commands.Update;

public class UpdateReviewCommand : IRequest<UpdatedReviewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public string ReviewTitle { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewContent { get; set; }

    public string[] Roles => [Admin, Write, ReviewsOperationClaims.Update, ReviewsOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReviews"];

    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, UpdatedReviewResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly ReviewBusinessRules _reviewBusinessRules;

        public UpdateReviewCommandHandler(IMapper mapper, IReviewRepository reviewRepository,
                                         ReviewBusinessRules reviewBusinessRules)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _reviewBusinessRules = reviewBusinessRules;
        }

        public async Task<UpdatedReviewResponse> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _reviewBusinessRules.ReviewShouldExistWhenSelected(review);
            review = _mapper.Map(request, review);

            await _reviewRepository.UpdateAsync(review!);

            UpdatedReviewResponse response = _mapper.Map<UpdatedReviewResponse>(review);
            return response;
        }
    }
}