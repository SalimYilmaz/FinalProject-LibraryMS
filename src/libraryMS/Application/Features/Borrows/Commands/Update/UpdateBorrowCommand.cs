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

namespace Application.Features.Borrows.Commands.Update;

public class UpdateBorrowCommand : IRequest<UpdatedBorrowResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime ReturnDate { get; set; }

    public string[] Roles => [Admin, Write, BorrowsOperationClaims.Update, BorrowsOperationClaims.GeneralEmployee,BorrowsOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBorrows"];

    public class UpdateBorrowCommandHandler : IRequestHandler<UpdateBorrowCommand, UpdatedBorrowResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBorrowRepository _borrowRepository;
        private readonly BorrowBusinessRules _borrowBusinessRules;

        public UpdateBorrowCommandHandler(IMapper mapper, IBorrowRepository borrowRepository,
                                         BorrowBusinessRules borrowBusinessRules)
        {
            _mapper = mapper;
            _borrowRepository = borrowRepository;
            _borrowBusinessRules = borrowBusinessRules;
        }

        public async Task<UpdatedBorrowResponse> Handle(UpdateBorrowCommand request, CancellationToken cancellationToken)
        {
            Borrow? borrow = await _borrowRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _borrowBusinessRules.BorrowShouldExistWhenSelected(borrow);

            borrow = _mapper.Map(request, borrow);
            await _borrowRepository.UpdateAsync(borrow!);

            UpdatedBorrowResponse response = _mapper.Map<UpdatedBorrowResponse>(borrow);
            return response;
        }
    }
}