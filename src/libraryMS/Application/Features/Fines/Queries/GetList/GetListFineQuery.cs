using Application.Features.Fines.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Fines.Constants.FinesOperationClaims;
using Application.Services.Members;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Application.Features.Auth.Constants;
using Application.Services.OperationClaims;
using Application.Services.UserOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Features.Fines.Rules;

namespace Application.Features.Fines.Queries.GetList;

public class GetListFineQuery : IRequest<GetListResponse<GetListFineListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid? UserId{ get; set; }
    public string[] CurrentUserRoles { get; set; }
    public string[] Roles => [Admin, Read,FinesOperationClaims.Member, FinesOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListFines({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetFines";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListFineQueryHandler : IRequestHandler<GetListFineQuery, GetListResponse<GetListFineListItemDto>>
    {
        private readonly IFineRepository _fineRepository;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;
        private readonly FineBusinessRules _fineBusinessRules;

        public GetListFineQueryHandler(IFineRepository fineRepository, IMapper mapper, IMemberService memberService, FineBusinessRules fineBusinessRules)
        {
            _fineRepository = fineRepository;
            _mapper = mapper;
            _memberService = memberService;
            _fineBusinessRules = fineBusinessRules;
        }

        public async Task<GetListResponse<GetListFineListItemDto>> Handle(GetListFineQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Fine> fines = null!;

            fines = await _fineBusinessRules.ListUserFinesByRoleWithPagination(
                request.CurrentUserRoles,
                request.PageRequest.PageIndex,
                request.PageRequest.PageSize,request.UserId,cancellationToken);

            GetListResponse<GetListFineListItemDto> response = _mapper.Map<GetListResponse<GetListFineListItemDto>>(fines);
            return response;
        }
    }
}