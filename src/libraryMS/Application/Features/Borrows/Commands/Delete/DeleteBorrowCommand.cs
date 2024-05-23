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
using Application.Features.Fines.Queries.GetList;
using Microsoft.EntityFrameworkCore;
using Application.Features.Auth.Dtos;
using Application.Services.Items;
using Application.Features.Fines.Rules;

namespace Application.Features.Borrows.Commands.Delete;

public class DeleteBorrowCommand : IRequest<DeletedBorrowResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid? Id { get; set; }
    public string[] Roles => [Admin, Write, BorrowsOperationClaims.Delete, BorrowsOperationClaims.GeneralEmployee];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBorrows"];

    public class DeleteBorrowCommandHandler : IRequestHandler<DeleteBorrowCommand, DeletedBorrowResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBorrowRepository _borrowRepository;
        private readonly BorrowBusinessRules _borrowBusinessRules;
        private readonly IItemService _itemService;
        private readonly FineBusinessRules _fineBusinessRules;

        public DeleteBorrowCommandHandler(IMapper mapper, IBorrowRepository borrowRepository,
            BorrowBusinessRules borrowBusinessRules, IItemService itemService, FineBusinessRules fineBusinessRules)
        {
            _mapper = mapper;
            _borrowRepository = borrowRepository;
            _borrowBusinessRules = borrowBusinessRules;
            _itemService = itemService;
            _fineBusinessRules = fineBusinessRules;
        }

        public async Task<DeletedBorrowResponse> Handle(DeleteBorrowCommand request, CancellationToken cancellationToken)
        {
            Borrow? borrow = await _borrowRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _borrowBusinessRules.BorrowShouldExistWhenSelected(borrow);

            await _fineBusinessRules.CheckAndApplyFineIfRequired(borrow.ReturnDate,borrow.Id,borrow.MemberId);

            await _borrowRepository.DeleteAsync(borrow!);
            await _itemService.setCurrentItemInStock(borrow.ItemId,stockUpdateDirection : true);
            DeletedBorrowResponse response = _mapper.Map<DeletedBorrowResponse>(borrow);
            return response;
        }
    }
}