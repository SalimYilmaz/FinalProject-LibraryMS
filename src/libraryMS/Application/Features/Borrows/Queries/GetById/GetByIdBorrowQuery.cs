using Application.Features.Borrows.Constants;
using Application.Features.Borrows.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Borrows.Constants.BorrowsOperationClaims;
using Application.Services.Members;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Application.Features.Auth.Constants;
using Application.Services.OperationClaims;
using Application.Services.UserOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Borrows.Queries.GetById;

public class GetByIdBorrowQuery : IRequest<GetByIdBorrowResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string[] Roles => [Admin, Read,BorrowsOperationClaims.Member];

    public class GetByIdBorrowQueryHandler : IRequestHandler<GetByIdBorrowQuery, GetByIdBorrowResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBorrowRepository _borrowRepository;
        private readonly BorrowBusinessRules _borrowBusinessRules;
        public GetByIdBorrowQueryHandler(IMapper mapper, IBorrowRepository borrowRepository, BorrowBusinessRules borrowBusinessRules)
        {
            _mapper = mapper;
            _borrowRepository = borrowRepository;
            _borrowBusinessRules = borrowBusinessRules;
        }

        public async Task<GetByIdBorrowResponse> Handle(GetByIdBorrowQuery request, CancellationToken cancellationToken)
        {
            Borrow? borrow = await _borrowRepository.GetAsync(
                predicate: b => b.Id == request.Id, 
                cancellationToken: cancellationToken,
                include : b => b.Include(b => b.Item).Include(b => b.Member)!);

            await _borrowBusinessRules.BorrowShouldExistWhenSelected(borrow);

            GetByIdBorrowResponse response = _mapper.Map<GetByIdBorrowResponse>(borrow);
            return response;
        }
    }
}