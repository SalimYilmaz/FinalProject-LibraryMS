using Application.Features.Items.Queries.GetDynamic;
using Application.Features.Members.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Application.Pipelines.Authorization;
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

namespace Application.Features.Members.Queries.GetDynamic;
public  class GetDynamicMemberQuery : MediatR.IRequest<GetListResponse<GetDynamicMemberItemDto>>, ICachableRequest, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetDynamicMembers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetMembers";
    public TimeSpan? SlidingExpiration { get; }

    public string[] Roles => [MembersOperationClaims.Admin, MembersOperationClaims.Read, MembersOperationClaims.Employee];

    public class GetDynamicMemberQueryHandler : IRequestHandler<GetDynamicMemberQuery, GetListResponse<GetDynamicMemberItemDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetDynamicMemberQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetDynamicMemberItemDto>> Handle(GetDynamicMemberQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Member> items = await _memberRepository.GetListByDynamicAsync
                (request.Dynamic, index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize); 


            GetListResponse<GetDynamicMemberItemDto> response = _mapper.Map<GetListResponse<GetDynamicMemberItemDto>>(items);

            return response;

        }
    
    }
}
