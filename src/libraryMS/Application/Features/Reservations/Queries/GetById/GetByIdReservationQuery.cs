using Application.Features.Reservations.Constants;
using Application.Features.Reservations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Reservations.Constants.ReservationsOperationClaims;
using Application.Features.Auth.Constants;
using Application.Services.Members;
using Application.Services.OperationClaims;
using Application.Services.UserOperationClaims;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Persistence.Paging;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Queries.GetById;

public class GetByIdReservationQuery : IRequest<GetByIdReservationResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read,ReservationsOperationClaims.Member];

    public class GetByIdReservationQueryHandler : IRequestHandler<GetByIdReservationQuery, GetByIdReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly ReservationBusinessRules _reservationBusinessRules;
        public GetByIdReservationQueryHandler(IMapper mapper, IReservationRepository reservationRepository, ReservationBusinessRules reservationBusinessRules)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _reservationBusinessRules = reservationBusinessRules;
        }

        public async Task<GetByIdReservationResponse> Handle(GetByIdReservationQuery request, CancellationToken cancellationToken)
        {
            Reservation? reservation = await _reservationRepository.GetAsync(r => r.Id.Equals(request.Id), 
                cancellationToken : cancellationToken,
                include : r => r.Include(r => r.Item).Include(r => r.Member)!);
            await _reservationBusinessRules.ReservationShouldExistWhenSelected(reservation);

            GetByIdReservationResponse response = _mapper.Map<GetByIdReservationResponse>(reservation);
            return response;
        }
    }
}