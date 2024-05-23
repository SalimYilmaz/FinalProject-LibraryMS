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
using System.Diagnostics;

namespace Application.Features.Fines.Commands.Create;

public class CreateFineCommand : IRequest<CreatedFineResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid MemberId { get; set; }
    public Guid BorrowId { get; set; }
    public decimal FineAmount { get; set; }
    public string FineType { get; set; }
    public string Description { get; set; }
    public bool IsPaid { get; set; }

    public string[] Roles => [Admin, Write, FinesOperationClaims.Create, FinesOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFines"];

    public class CreateFineCommandHandler : IRequestHandler<CreateFineCommand, CreatedFineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFineRepository _fineRepository;
        private readonly FineBusinessRules _fineBusinessRules;

        public CreateFineCommandHandler(IMapper mapper, IFineRepository fineRepository,
                                         FineBusinessRules fineBusinessRules)
        {
            _mapper = mapper;
            _fineRepository = fineRepository;
            _fineBusinessRules = fineBusinessRules;
        }

        public async Task<CreatedFineResponse> Handle(CreateFineCommand request, CancellationToken cancellationToken)
        {
            Fine fine = _mapper.Map<Fine>(request);

            await _fineRepository.AddAsync(fine);
            CreatedFineResponse response = _mapper.Map<CreatedFineResponse>(fine);
            return  response;
        }
    }
}