using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Reviews.Constants.ReviewsOperationClaims;

namespace Application.Features.Reviews.Queries.GetList;

public class GetListReviewQuery : IRequest<GetListResponse<GetListReviewListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListReviews({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetReviews";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListReviewQueryHandler : IRequestHandler<GetListReviewQuery, GetListResponse<GetListReviewListItemDto>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GetListReviewQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListReviewListItemDto>> Handle(GetListReviewQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Review> reviews = await _reviewRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListReviewListItemDto> response = _mapper.Map<GetListResponse<GetListReviewListItemDto>>(reviews);
            return response;
        }
    }
}