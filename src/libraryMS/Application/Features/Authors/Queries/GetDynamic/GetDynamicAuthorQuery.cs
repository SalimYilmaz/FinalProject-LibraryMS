using Application.Features.Items.Queries.GetDynamic;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
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

namespace Application.Features.Authors.Queries.GetDynamic;
public class GetDynamicAuthorQuery : MediatR.IRequest<GetListResponse<GetDynamicAuthorItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetDynamicAuthors({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAuthors";
    public TimeSpan? SlidingExpiration { get; }

    public class GetDynamicAuthorQueryHandler : IRequestHandler<GetDynamicAuthorQuery, GetListResponse<GetDynamicAuthorItemDto>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetDynamicAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetDynamicAuthorItemDto>> Handle(GetDynamicAuthorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Author> authors = await _authorRepository.GetListByDynamicAsync
                (request.Dynamic, index: request.PageRequest.PageIndex, 
                size: request.PageRequest.PageSize, 
                cancellationToken : cancellationToken);

            GetListResponse<GetDynamicAuthorItemDto> response = _mapper.Map<GetListResponse<GetDynamicAuthorItemDto>>(authors);

            return response;
        }
    }
}

