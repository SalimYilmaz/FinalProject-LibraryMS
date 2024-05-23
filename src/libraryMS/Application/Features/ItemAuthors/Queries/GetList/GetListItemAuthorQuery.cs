using Application.Features.ItemAuthors.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ItemAuthors.Constants.ItemAuthorsOperationClaims;

namespace Application.Features.ItemAuthors.Queries.GetList;

public class GetListItemAuthorQuery : IRequest<GetListResponse<GetListItemAuthorListItemDto>>,  ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey => $"GetListItemAuthors({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetItemAuthors";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListItemAuthorQueryHandler : IRequestHandler<GetListItemAuthorQuery, GetListResponse<GetListItemAuthorListItemDto>>
    {
        private readonly IItemAuthorRepository _itemAuthorRepository;
        private readonly IMapper _mapper;

        public GetListItemAuthorQueryHandler(IItemAuthorRepository itemAuthorRepository, IMapper mapper)
        {
            _itemAuthorRepository = itemAuthorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListItemAuthorListItemDto>> Handle(GetListItemAuthorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ItemAuthor> itemAuthors = await _itemAuthorRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListItemAuthorListItemDto> response = _mapper.Map<GetListResponse<GetListItemAuthorListItemDto>>(itemAuthors);
            return response;
        }
    }
}