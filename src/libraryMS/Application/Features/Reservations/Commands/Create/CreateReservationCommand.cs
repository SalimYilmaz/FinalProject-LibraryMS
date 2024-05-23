using Application.Features.Reservations.Constants;
using Application.Features.Reservations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Reservations.Constants.ReservationsOperationClaims;
using Application.Features.Items.Rules;
using Application.Services.Items;

namespace Application.Features.Reservations.Commands.Create;

public class CreateReservationCommand : IRequest<CreatedReservationResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime ReservationDate { get; set; }

    public string[] Roles => [Admin, Write, ReservationsOperationClaims.Create, ReservationsOperationClaims.Employee, ReservationsOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReservations"];

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, CreatedReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly ReservationBusinessRules _reservationBusinessRules;
        private readonly ItemBusinessRules _itemBusinessRules;
        private readonly IItemService _itemService;

        public CreateReservationCommandHandler(IMapper mapper, IReservationRepository reservationRepository,
                                         ReservationBusinessRules reservationBusinessRules, ItemBusinessRules itemBusinessRules, IItemService itemService)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _reservationBusinessRules = reservationBusinessRules;
            _itemBusinessRules = itemBusinessRules;
            _itemService = itemService;
        }

        public async Task<CreatedReservationResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = _mapper.Map<Reservation>(request);

            await _reservationRepository.AddAsync(reservation);

            CreatedReservationResponse response = _mapper.Map<CreatedReservationResponse>(reservation);
            return response;
        }
    }
}