using Application.Features.Fines.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Features.Auth.Dtos;
using Application.Services.Fines;
using AutoMapper;
using Nest;
using Application.Features.Auth.Constants;
using Org.BouncyCastle.Asn1.Ocsp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using NArchitecture.Core.Persistence.Paging;
using Application.Services.Members;
using System.Linq.Expressions;

namespace Application.Features.Fines.Rules;

public class FineBusinessRules : BaseBusinessRules
{
    private readonly IFineRepository _fineRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    private readonly IMemberService _memberService;

    public FineBusinessRules(IFineRepository fineRepository, IMapper mapper, ILocalizationService localizationService, IMemberService memberService)
    {
        _fineRepository = fineRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _memberService = memberService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, FinesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task FineShouldExistWhenSelected(Fine? fine)
    {
        if (fine == null)
            await throwBusinessException(FinesBusinessMessages.FineNotExists);
    }

    public async Task FineIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Fine? fine = await _fineRepository.GetAsync(
            predicate: f => f.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FineShouldExistWhenSelected(fine);
    }
  
    public async Task CheckAndApplyFineIfRequired(DateTime returnDate,Guid borrowId,Guid memberId)
    {
        if (returnDate < DateTime.Today)
        {
            double fineAmount = (double)(returnDate - DateTime.Today).TotalDays * 25;
            Fine fine = new Fine() { BorrowId = borrowId,MemberId = memberId,FineAmount = fineAmount };
            await _fineRepository.AddAsync(fine);
            await _memberService.UpdateMemberDebtByIdAsync(memberId,fineAmount);
        }

    }
    public async Task<IPaginate<Fine>> ListUserFinesByRoleWithPagination(string [] currentUserRoles,int pageIndex,int pageSize,Guid? userId,CancellationToken cancellationToken)
    {
        bool isAdmin = (currentUserRoles.Contains(AuthOperationClaims.GeneralAdmin) ||
        currentUserRoles.Contains(FinesOperationClaims.Admin) ||
        currentUserRoles.Contains(FinesOperationClaims.Employee)) ? true : false;

        if (isAdmin)
        {
            return  await _fineRepository.GetListAsync(
            index: pageIndex,
            size: pageSize,
            cancellationToken: cancellationToken,
            include: f => f.Include(f => f.Member)!.Include(f => f.Borrow)!);
        }
        else
        {
            Member? member = await _memberService.GetAsync(m => m.UserId.Equals(userId));
            return await _fineRepository.GetListAsync(
            index: pageIndex,
            size: pageSize,
            cancellationToken: cancellationToken,
            predicate: f => f.MemberId.Equals(member!.Id),
            include: f => f.Include(f => f.Member).Include(f => f.Borrow)!);
        }
    }
}