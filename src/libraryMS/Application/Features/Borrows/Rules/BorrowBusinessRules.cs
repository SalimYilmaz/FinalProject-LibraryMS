using Application.Features.Borrows.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Nest;
using Application.Services.Fines;
using Application.Features.Borrows.Commands.Delete;
using Application.Features.Fines.Queries.GetList;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Application.Features.Auth.Dtos;
using Org.BouncyCastle.Asn1.Ocsp;
using Application.Services.Members;
using MediatR;
using System.Threading;
using NArchitecture.Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Application.Features.Auth.Constants;

namespace Application.Features.Borrows.Rules;

public class BorrowBusinessRules : BaseBusinessRules
{
    private readonly IBorrowRepository _borrowRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMemberService _memberService;


    public BorrowBusinessRules(IBorrowRepository borrowRepository, ILocalizationService localizationService, IMemberService memberService)
    {
        _borrowRepository = borrowRepository;
        _localizationService = localizationService;
        _memberService = memberService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BorrowsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BorrowShouldExistWhenSelected(Borrow? borrow)
    {
        if (borrow == null)
            await throwBusinessException(BorrowsBusinessMessages.BorrowNotExists);
    }
    public async Task BorrowsShouldExistWhenSelected(IPaginate<Borrow>? borrowList)
    {
        if (borrowList == null)
            await throwBusinessException(BorrowsBusinessMessages.BorrowNotExists);
    }


    public async Task BorrowIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Borrow? borrow = await _borrowRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BorrowShouldExistWhenSelected(borrow);
    }
    private async Task<IPaginate<Borrow>> paginateCurrentUserBorrowsForProfile(Guid userId,int pageIndex,int pageSize, CancellationToken cancellationToken)
    {
        Member? member = await _memberService.GetAsync(m => m.UserId.Equals(userId));
        return
            await _borrowRepository.GetListAsync(
                index: pageIndex,
                size: pageSize,
                cancellationToken: cancellationToken,
                predicate: b => b.MemberId.Equals(member!.Id),
                include :  b => b.Include(b => b.Item)!);
    }
    private async Task<IPaginate<Borrow>> getUserBorrowsForAdmin(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _borrowRepository.GetListAsync(
            index: pageIndex,
            size: pageSize,
            cancellationToken: cancellationToken,
            include: b => b.Include(b => b.Member).Include(b => b.Item)!);
    }
    private async Task<IPaginate<Borrow>> getAllUserBorrowsForReservationWithPagination(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _borrowRepository.GetListAsync(
            index: pageIndex,
            size: pageSize,
            cancellationToken: cancellationToken,
            include: b => b.Include(b => b.Item)!);
;
    }
    public async Task<IPaginate<Borrow>> ListUserBorrowsByRoleWithPagination(Guid userId,int pageIndex,int pageSize,bool isUserInProfile,string[] currentUserRoles,CancellationToken cancellationToken)
    {
        bool isAdmin = currentUserRoles.Contains(AuthOperationClaims.GeneralAdmin) ||
             currentUserRoles.Contains(BorrowsOperationClaims.GeneralEmployee) ||
             currentUserRoles.Contains(BorrowsOperationClaims.Admin) ? true : false;
        if (isUserInProfile is not true && !isAdmin)
        {
            return await getAllUserBorrowsForReservationWithPagination(pageIndex,pageSize,cancellationToken);
        }
        else if (isUserInProfile && !isAdmin)
        {
            return await paginateCurrentUserBorrowsForProfile(userId,pageIndex,pageSize,cancellationToken);
        }
        else
        {
            return await getUserBorrowsForAdmin(pageIndex, pageSize, cancellationToken);
        }
    }
}