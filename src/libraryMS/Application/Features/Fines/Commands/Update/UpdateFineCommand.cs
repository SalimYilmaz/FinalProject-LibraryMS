using Application.Features.Fines.Constants;
using Application.Features.Fines.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Fines.Constants.FinesOperationClaims;

namespace Application.Features.Fines.Commands.Update;

public class UpdateFineCommand : IRequest<UpdatedFineResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid BorrowId { get; set; }
    public decimal FineAmount { get; set; }
    public string FineType { get; set; }
    public DateTime IssueDate { get; set; }
    public string Description { get; set; }
    public bool Paid { get; set; }

    public string[] Roles => [Admin, Write, FinesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFines"];

    public class UpdateFineCommandHandler : IRequestHandler<UpdateFineCommand, UpdatedFineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFineRepository _fineRepository;
        private readonly FineBusinessRules _fineBusinessRules;

        public UpdateFineCommandHandler(IMapper mapper, IFineRepository fineRepository,
                                         FineBusinessRules fineBusinessRules)
        {
            _mapper = mapper;
            _fineRepository = fineRepository;
            _fineBusinessRules = fineBusinessRules;
        }

        public async Task<UpdatedFineResponse> Handle(UpdateFineCommand request, CancellationToken cancellationToken)
        {
            Fine? fine = await _fineRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _fineBusinessRules.FineShouldExistWhenSelected(fine);
            fine = _mapper.Map(request, fine);

            await _fineRepository.UpdateAsync(fine!);

            UpdatedFineResponse response = _mapper.Map<UpdatedFineResponse>(fine);
            return response;
        }
    }
}