using Application.Features.Fines.Constants;
using Application.Features.Fines.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Fines.Constants.FinesOperationClaims;
using Application.Features.Auth.Constants;
using Application.Services.Members;
using Application.Services.OperationClaims;
using Application.Services.UserOperationClaims;
using NArchitecture.Core.Persistence.Paging;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Fines.Queries.GetById;

public class GetByIdFineQuery : IRequest<GetByIdFineResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read,FinesOperationClaims.Member, FinesOperationClaims.Employee];

    public class GetByIdFineQueryHandler : IRequestHandler<GetByIdFineQuery, GetByIdFineResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFineRepository _fineRepository;
        private readonly FineBusinessRules _fineBusinessRules;

        public GetByIdFineQueryHandler(IMapper mapper, IFineRepository fineRepository, FineBusinessRules fineBusinessRules)
        {
            _mapper = mapper;
            _fineRepository = fineRepository;
            _fineBusinessRules = fineBusinessRules;
        }


        public async Task<GetByIdFineResponse> Handle(GetByIdFineQuery request, CancellationToken cancellationToken)
        {
            Fine? fine = await _fineRepository.GetAsync(predicate: f => f.Id == request.Id, 
                cancellationToken: cancellationToken,
                include : f=>f.Include(f=>f.Borrow).Include(f=>f.Member)!);

            await _fineBusinessRules.FineShouldExistWhenSelected(fine);

            GetByIdFineResponse response = _mapper.Map<GetByIdFineResponse>(fine);
            return response;
        }
    }
}