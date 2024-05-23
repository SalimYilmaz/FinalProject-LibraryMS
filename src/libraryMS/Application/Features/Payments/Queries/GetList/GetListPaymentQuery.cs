using Application.Features.Payments.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Payments.Constants.PaymentsOperationClaims;
using Application.Features.Payments.Rules;

namespace Application.Features.Payments.Queries.GetList;

public class GetListPaymentQuery : IRequest<GetListResponse<GetListPaymentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] CurrentUserRoles { get; set; }
    public Guid UserId { get; set; }
    public string[] Roles => [Admin, Read,PaymentsOperationClaims.Member, PaymentsOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListPayments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetPayments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPaymentQueryHandler : IRequestHandler<GetListPaymentQuery, GetListResponse<GetListPaymentListItemDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly PaymentBusinessRules _paymentBusinessRules;

        public GetListPaymentQueryHandler(IPaymentRepository paymentRepository, IMapper mapper, PaymentBusinessRules paymentBusinessRules)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _paymentBusinessRules = paymentBusinessRules;
        }

        public async Task<GetListResponse<GetListPaymentListItemDto>> Handle(GetListPaymentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Payment> payments = 
                await _paymentBusinessRules.ListMemberPaymentsByRoleWithPagination(request.PageRequest.PageSize, request.PageRequest.PageIndex, request.CurrentUserRoles,request.UserId);

            GetListResponse<GetListPaymentListItemDto> response = _mapper.Map<GetListResponse<GetListPaymentListItemDto>>(payments);
            return response;
        }
    }
}