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

namespace Application.Features.Reviews.Commands.Delete;

public class DeleteReviewCommand : IRequest<DeletedReviewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ReviewsOperationClaims.Delete, ReviewsOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReviews"];

    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, DeletedReviewResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly ReviewBusinessRules _reviewBusinessRules;

        public DeleteReviewCommandHandler(IMapper mapper, IReviewRepository reviewRepository,
                                         ReviewBusinessRules reviewBusinessRules)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _reviewBusinessRules = reviewBusinessRules;
        }

        public async Task<DeletedReviewResponse> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _reviewBusinessRules.ReviewShouldExistWhenSelected(review);

            await _reviewRepository.DeleteAsync(review!);

            DeletedReviewResponse response = _mapper.Map<DeletedReviewResponse>(review);
            return response;
        }
    }
}