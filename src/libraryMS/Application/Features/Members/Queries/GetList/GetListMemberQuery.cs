using Application.Features.Members.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Members.Constants.MembersOperationClaims;

namespace Application.Features.Members.Queries.GetList;

public class GetListMemberQuery : IRequest<GetListResponse<GetListMemberItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read, MembersOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListMembers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetMembers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListMemberQueryHandler : IRequestHandler<GetListMemberQuery, GetListResponse<GetListMemberItemDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetListMemberQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMemberItemDto>> Handle(GetListMemberQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Member> members = await _memberRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMemberItemDto> response = _mapper.Map<GetListResponse<GetListMemberItemDto>>(members);
            return response;
        }
    }
}