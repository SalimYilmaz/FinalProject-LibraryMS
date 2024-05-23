using Application.Features.Payments.Constants;
using Application.Features.Payments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Payments.Constants.PaymentsOperationClaims;
using Application.Features.LibraryMembers.Constants;
using Application.Services.PaymentService;
using System.Text.Json.Serialization;

namespace Application.Features.Payments.Commands.Create;

public class CreatePaymentCommand : IRequest<CreatedPaymentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string CreditCartHolderName { get; set; }
    public string CreditCartHolderSurname { get; set; }
    public string CreditCardNo { get; set; }
    public string Cvc { get; set; }
    public DateTime ExpiryDate { get; set; }
    public Guid MemberId { get; set; }
    [JsonIgnore]
    public string? IpAddress { get; set; } = "";
    public string[] Roles => [Admin, Write, PaymentsOperationClaims.Create, PaymentsOperationClaims.Member, PaymentsOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPayments"];

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatedPaymentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly PaymentBusinessRules _paymentBusinessRules;
        private readonly PaymentServiceBase _paymentServiceBase;
        public CreatePaymentCommandHandler(IMapper mapper, IPaymentRepository paymentRepository,
                                         PaymentBusinessRules paymentBusinessRules, PaymentServiceBase paymentServiceBase)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _paymentBusinessRules = paymentBusinessRules;
            _paymentServiceBase = paymentServiceBase;
        }

        public async Task<CreatedPaymentResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            Payment payment = _mapper.Map<Payment>(request);

            await _paymentRepository.AddAsync(payment);

            await _paymentServiceBase.Pay(request.CreditCartHolderName, request.CreditCartHolderSurname, request.CreditCardNo, request.Cvc, request.MemberId,request.IpAddress, request.ExpiryDate);
            CreatedPaymentResponse response = _mapper.Map<CreatedPaymentResponse>(payment);
            return response;
        }
    }
}