using Application.Features.Reservations.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Reservations.Constants.ReservationsOperationClaims;
using Application.Features.Members.Rules;
using Application.Features.Reservations.Rules;
using Application.Services.Members;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Application.Services.UsersService;
using Application.Features.Auth.Constants;
using Application.Services.OperationClaims;
using Application.Services.UserOperationClaims;

namespace Application.Features.Reservations.Queries.GetList;

public class GetListReservationQuery : IRequest<GetListResponse<GetListReservationListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid UserId {  get; set; }

    public string [] CurrentUserRoles { get; set; }
    public string[] Roles => [Admin, Read, ReservationsOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListReservations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetReservations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListReservationQueryHandler : IRequestHandler<GetListReservationQuery, GetListResponse<GetListReservationListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ReservationBusinessRules _reservationBusinessRules;

        public GetListReservationQueryHandler(IMapper mapper, ReservationBusinessRules reservationBusinessRules)
        {
            _mapper = mapper;
            _reservationBusinessRules = reservationBusinessRules;
        }

        public async Task<GetListResponse<GetListReservationListItemDto>> Handle(GetListReservationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Reservation> reservations = await _reservationBusinessRules.ListUserReservationsByRoleWithPagination(request.CurrentUserRoles,
                request.PageRequest.PageIndex,
                request.PageRequest.PageSize,
                request.UserId,
                cancellationToken);
           
            GetListResponse<GetListReservationListItemDto> response = _mapper.Map<GetListResponse<GetListReservationListItemDto>>(reservations);
            return response;

        }
    }
}