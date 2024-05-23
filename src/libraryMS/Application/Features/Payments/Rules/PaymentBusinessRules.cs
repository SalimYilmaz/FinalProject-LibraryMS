using Application.Features.Payments.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Features.Auth.Constants;
using Application.Features.Borrows.Constants;
using NArchitecture.Core.Security.Entities;
using System.Threading;
using NArchitecture.Core.Persistence.Paging;
using Application.Services.Members;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.Rules;

public class PaymentBusinessRules : BaseBusinessRules
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMemberService _memberService;

    public PaymentBusinessRules(IPaymentRepository paymentRepository, ILocalizationService localizationService, IMemberService memberService = null)
    {
        _paymentRepository = paymentRepository;
        _localizationService = localizationService;
        _memberService = memberService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, PaymentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task PaymentShouldExistWhenSelected(Payment? payment)
    {
        if (payment == null)
            await throwBusinessException(PaymentsBusinessMessages.PaymentNotExists);
    }

    public async Task PaymentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Payment? payment = await _paymentRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PaymentShouldExistWhenSelected(payment);
    }
    public async Task<IPaginate<Payment>> ListMemberPaymentsByRoleWithPagination(int pageSize,int pageIndex, string[] currentUserRoles,Guid userId)
    {
        bool isAdmin = currentUserRoles.Contains(AuthOperationClaims.GeneralAdmin) ||
             currentUserRoles.Contains(BorrowsOperationClaims.GeneralEmployee) ||
             currentUserRoles.Contains(BorrowsOperationClaims.Admin) ? true : false;
        if (isAdmin)
        {
            return await _paymentRepository.GetListAsync(size : pageSize, 
                index : pageIndex,
                include :
                p => p.Include(p => p.Member));
        }
        else
        {
            Member? member = await _memberService.GetAsync(m => m.UserId.Equals(userId) );

            return await _paymentRepository.GetListAsync(
                index : pageIndex,
                size : pageSize,
                predicate : p => p.MemberId.Equals(member!.Id),include : m => m.Include(m => m.Member));
        }
    }
}