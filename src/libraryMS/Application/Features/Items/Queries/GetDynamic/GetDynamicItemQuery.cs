using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;
using NArchitecture.Core.Persistence.Repositories;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Items.Queries.GetDynamic;
public  class GetDynamicItemQuery : MediatR.IRequest<GetListResponse<GetDynamicItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey => $"GetDynamicItems({PageRequest.PageIndex}{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetItems";

    public TimeSpan? SlidingExpiration { get; }

    public class GetDynamicItemQueryHandler : IRequestHandler<GetDynamicItemQuery, GetListResponse<GetDynamicItemDto>>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public GetDynamicItemQueryHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetDynamicItemDto>> Handle(GetDynamicItemQuery request, CancellationToken cancellationToken)
        {
           
            IPaginate<Item> items = await _itemRepository.GetListByDynamicAsync
                (request.Dynamic,
                index: request.PageRequest.PageIndex, 
                size: request.PageRequest.PageSize,
                include : i=>i.Include(i=>i.Publisher).Include(i=>i.Location).Include(i=>i.Library)!);

            GetListResponse<GetDynamicItemDto> response = _mapper.Map<GetListResponse<GetDynamicItemDto>>(items);

            return response;

        }
    }
}
