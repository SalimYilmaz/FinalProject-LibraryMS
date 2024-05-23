using Application.Features.LibraryMembers.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.LibraryMembers.Constants.LibraryMembersOperationClaims;

namespace Application.Features.LibraryMembers.Queries.GetList;

public class GetListLibraryMemberQuery : IRequest<GetListResponse<GetListLibraryMemberListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListLibraryMembers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetLibraryMembers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLibraryMemberQueryHandler : IRequestHandler<GetListLibraryMemberQuery, GetListResponse<GetListLibraryMemberListItemDto>>
    {
        private readonly ILibraryMemberRepository _libraryMemberRepository;
        private readonly IMapper _mapper;

        public GetListLibraryMemberQueryHandler(ILibraryMemberRepository libraryMemberRepository, IMapper mapper)
        {
            _libraryMemberRepository = libraryMemberRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLibraryMemberListItemDto>> Handle(GetListLibraryMemberQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LibraryMember> libraryMembers = await _libraryMemberRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLibraryMemberListItemDto> response = _mapper.Map<GetListResponse<GetListLibraryMemberListItemDto>>(libraryMembers);
            return response;
        }
    }
}