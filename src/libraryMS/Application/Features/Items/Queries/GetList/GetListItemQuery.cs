using Application.Features.Items.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Items.Constants.ItemsOperationClaims;
using Application.Features.Items.Rules;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;
using Nest;
using YamlDotNet.Core;
using Application.Services.Reservations;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Items.Queries.GetList;

public class GetListItemQuery : MediatR.IRequest<GetListResponse<GetListItemListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey => $"GetListItems({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetItems";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListItemQueryHandler : IRequestHandler<GetListItemQuery, GetListResponse<GetListItemListItemDto>>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public GetListItemQueryHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListItemListItemDto>> Handle(GetListItemQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Item>? items = await _itemRepository.GetListAsync
                  (index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize);

            GetListResponse<GetListItemListItemDto> response = _mapper.Map
                <GetListResponse<GetListItemListItemDto>>(items);
            return response;
        }
    }
}