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

namespace Application.Features.Reviews.Commands.Create;

public class CreateReviewCommand : IRequest<CreatedReviewResponse>,  ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public string ReviewTitle { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewContent { get; set; }

    public string[] Roles => [Admin, Write, ReviewsOperationClaims.Create, ReviewsOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReviews"];

    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreatedReviewResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly ReviewBusinessRules _reviewBusinessRules;

        public CreateReviewCommandHandler(IMapper mapper, IReviewRepository reviewRepository,
                                         ReviewBusinessRules reviewBusinessRules)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _reviewBusinessRules = reviewBusinessRules;
        }

        public async Task<CreatedReviewResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            Review review = _mapper.Map<Review>(request);

            await _reviewRepository.AddAsync(review);

            CreatedReviewResponse response = _mapper.Map<CreatedReviewResponse>(review);
            return response;
        }
    }
}