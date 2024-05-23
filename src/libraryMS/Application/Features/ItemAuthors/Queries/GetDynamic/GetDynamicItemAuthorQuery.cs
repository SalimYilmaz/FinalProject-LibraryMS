using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ItemAuthors.Queries.GetDynamic;
public class GetDynamicItemAuthorQuery : IRequest<GetListResponse<GetDynamicItemAuthorDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic {  get; set; }

    public bool BypassCache { get; }
    public string CacheKey => $"GetDynamicItemAuthors({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetItemAuthors";
    public TimeSpan? SlidingExpiration { get; }

    public class GetDynamicItemAuthorQueryHandler : IRequestHandler<GetDynamicItemAuthorQuery, GetListResponse<GetDynamicItemAuthorDto>>
    {
        private readonly IItemAuthorRepository _itemAuthorrepository;
        private readonly IMapper _mapper;
        public GetDynamicItemAuthorQueryHandler(IItemAuthorRepository itemAuthorrepository, IMapper mapper)
        {
            _itemAuthorrepository = itemAuthorrepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetDynamicItemAuthorDto>> Handle(GetDynamicItemAuthorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ItemAuthor> itemAuthors = await _itemAuthorrepository.GetListByDynamicAsync(request.Dynamic, 
                index : request.PageRequest.PageIndex,
                size : request.PageRequest.PageSize,
                include : ia => ia.Include(ia=>ia.Item).Include(ia=>ia.Author)!);

            GetListResponse<GetDynamicItemAuthorDto> response = _mapper.Map<GetListResponse<GetDynamicItemAuthorDto>>(itemAuthors);
            return response;
        }
    }
}
