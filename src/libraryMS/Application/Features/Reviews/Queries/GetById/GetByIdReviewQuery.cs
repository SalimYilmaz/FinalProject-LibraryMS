using Application.Features.Reviews.Constants;
using Application.Features.Reviews.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Reviews.Constants.ReviewsOperationClaims;

namespace Application.Features.Reviews.Queries.GetById;

public class GetByIdReviewQuery : IRequest<GetByIdReviewResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdReviewQueryHandler : IRequestHandler<GetByIdReviewQuery, GetByIdReviewResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly ReviewBusinessRules _reviewBusinessRules;

        public GetByIdReviewQueryHandler(IMapper mapper, IReviewRepository reviewRepository, ReviewBusinessRules reviewBusinessRules)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _reviewBusinessRules = reviewBusinessRules;
        }

        public async Task<GetByIdReviewResponse> Handle(GetByIdReviewQuery request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _reviewBusinessRules.ReviewShouldExistWhenSelected(review);

            GetByIdReviewResponse response = _mapper.Map<GetByIdReviewResponse>(review);
            return response;
        }
    }
}