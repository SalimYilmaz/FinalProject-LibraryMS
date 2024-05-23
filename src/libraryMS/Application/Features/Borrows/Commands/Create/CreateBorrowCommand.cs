using Application.Features.Borrows.Constants;
using Application.Features.Borrows.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Borrows.Constants.BorrowsOperationClaims;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Application.Services.Items;
using Application.Features.Items.Rules;
using Application.Services.Reservations;
using Application.Features.Reservations.Rules;

namespace Application.Features.Borrows.Commands.Create;

public class CreateBorrowCommand : IRequest<CreatedBorrowResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime ReturnDate { get; private set; } = DateTime.Today.AddDays(15);

    public string[] Roles => [Admin, Write, BorrowsOperationClaims.Create, BorrowsOperationClaims.Member, GeneralEmployee]; 

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBorrows"];

    public class CreateBorrowCommandHandler : IRequestHandler<CreateBorrowCommand, CreatedBorrowResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBorrowRepository _borrowRepository;
        private readonly IItemService _itemService;
        private readonly BorrowBusinessRules _borrowBusinessRules;
        private readonly IReservationService _reservationService;
        private readonly ReservationBusinessRules _reservationBusinessRules;
        private readonly ItemBusinessRules _itemBusinessRules;
        public CreateBorrowCommandHandler(IMapper mapper, IBorrowRepository borrowRepository,
                                         BorrowBusinessRules borrowBusinessRules, IItemService itemService, IReservationService reservationService, ReservationBusinessRules reservationBusinessRules, ItemBusinessRules itemBusinessRules)
        {
            _mapper = mapper;
            _borrowRepository = borrowRepository;
            _borrowBusinessRules = borrowBusinessRules;
            _itemService = itemService;
            _reservationService = reservationService;
            _reservationBusinessRules = reservationBusinessRules;
            _itemBusinessRules = itemBusinessRules;
        }

        public async Task<CreatedBorrowResponse> Handle(CreateBorrowCommand request, CancellationToken cancellationToken)
        {
            Borrow borrow = _mapper.Map<Borrow>(request);
            await _itemService.setCurrentItemInStock(request.ItemId, stockUpdateDirection: false);

            await _borrowRepository.AddAsync(borrow);

            CreatedBorrowResponse response = _mapper.Map<CreatedBorrowResponse>(borrow);
            return response;
        }
    }
}