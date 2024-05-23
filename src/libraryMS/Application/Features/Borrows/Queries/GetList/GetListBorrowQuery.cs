using Application.Features.Borrows.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Borrows.Constants.BorrowsOperationClaims;
using Application.Features.Borrows.Rules;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Application.Services.Members;
using Application.Features.Auth.Constants;
using Application.Services.OperationClaims;
using Application.Services.UserOperationClaims;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Security.Entities;
using Nest;

namespace Application.Features.Borrows.Queries.GetList;

public class GetListBorrowQuery : MediatR.IRequest<GetListResponse<GetListBorrowListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] CurrentUserRoles { get; set; }
    public bool IsUserInProfile { get; set; }
    public Guid UserId { get; set; }
    public string[] Roles => [Admin, Read, BorrowsOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBorrows({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBorrows";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBorrowQueryHandler : IRequestHandler<GetListBorrowQuery, GetListResponse<GetListBorrowListItemDto>>
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;
        private readonly BorrowBusinessRules _borrowBusinessRules;

        public GetListBorrowQueryHandler(IBorrowRepository borrowRepository, IMapper mapper, IMemberService memberService, BorrowBusinessRules borrowBusinessRules)
        {
            _borrowRepository = borrowRepository;
            _mapper = mapper;
            _memberService = memberService;
            _borrowBusinessRules = borrowBusinessRules;
        }

        public async Task<GetListResponse<GetListBorrowListItemDto>> Handle(GetListBorrowQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Borrow> borrows = await _borrowBusinessRules.ListUserBorrowsByRoleWithPagination(request.
                UserId,request.PageRequest.PageIndex, 
                request.PageRequest.PageSize,
                request.IsUserInProfile,
                request.CurrentUserRoles,cancellationToken);

            GetListResponse<GetListBorrowListItemDto> response = _mapper.Map<GetListResponse<GetListBorrowListItemDto>>(borrows);
            return response;
        }
    }
}