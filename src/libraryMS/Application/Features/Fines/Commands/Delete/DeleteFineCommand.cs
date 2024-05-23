using Application.Features.Fines.Constants;
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

namespace Application.Features.Fines.Commands.Delete;

public class DeleteFineCommand : IRequest<DeletedFineResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, FinesOperationClaims.Delete, FinesOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFines"];

    public class DeleteFineCommandHandler : IRequestHandler<DeleteFineCommand, DeletedFineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFineRepository _fineRepository;
        private readonly FineBusinessRules _fineBusinessRules;

        public DeleteFineCommandHandler(IMapper mapper, IFineRepository fineRepository,
                                         FineBusinessRules fineBusinessRules)
        {
            _mapper = mapper;
            _fineRepository = fineRepository;
            _fineBusinessRules = fineBusinessRules;
        }

        public async Task<DeletedFineResponse> Handle(DeleteFineCommand request, CancellationToken cancellationToken)
        {
            Fine? fine = await _fineRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _fineBusinessRules.FineShouldExistWhenSelected(fine);

            await _fineRepository.DeleteAsync(fine!);

            DeletedFineResponse response = _mapper.Map<DeletedFineResponse>(fine);
            return response;
        }
    }
}