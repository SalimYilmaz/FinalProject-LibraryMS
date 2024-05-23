using Application.Features.Reservations.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Features.Items.Rules;
using Application.Services.Items;
using Application.Services.Reservations;
using Application.Features.Auth.Constants;
using Application.Services.Members;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Threading;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Reservations.Rules;

public class ReservationBusinessRules : BaseBusinessRules
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IItemService _itemService;
    private readonly ItemBusinessRules _itemBusinessRules;
    private readonly IMemberService _memberService;

    public ReservationBusinessRules(IReservationRepository reservationRepository, ILocalizationService localizationService, IItemService itemService, ItemBusinessRules itemBusinessRules, IMemberService memberService)
    {
        _reservationRepository = reservationRepository;
        _localizationService = localizationService;
        _itemService = itemService;
        _itemBusinessRules = itemBusinessRules;
        _memberService = memberService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ReservationsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ReservationShouldExistWhenSelected(Reservation? reservation)
    {
        if (reservation == null)
            await throwBusinessException(ReservationsBusinessMessages.ReservationNotExists);
    }
    public async Task ReservationsShouldExistWhenSelected(IPaginate<Reservation>? reservationList)
    {
        if (reservationList!.Items is null)
            await throwBusinessException(ReservationsBusinessMessages.ReservationsCouldNotBeFound);
    }

    public async Task ReservationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Reservation? reservation = await _reservationRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ReservationShouldExistWhenSelected(reservation);
    }


    public async Task IsItemAvailableForReservation(Guid itemId, CancellationToken cancellationToken)
    {
        Item? item = await _itemService.GetAsync(
            predicate: i => i.Id == itemId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (item == null)
        {
            await _itemBusinessRules.ItemShouldExistWhenSelected(item);
        }
    }

    public async Task MemberCanMakeReservation(Guid memberId, CancellationToken cancellationToken)
    {
        DateTime today = DateTime.Today;

        var todaysReservations = await _reservationRepository.GetListAsync(
            predicate: r => r.MemberId == memberId && r.ReservationDate.Date == today.Date,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (todaysReservations.Items.Any())
        {
            await throwBusinessException(ReservationsBusinessMessages.ReservationAlreadyExistsForToday);
        }
    }
    public async Task<IPaginate<Reservation>> ListUserReservationsByRoleWithPagination(string[] currentUserRoles,int pageIndex, int pageSize,Guid userId,CancellationToken cancellationToken)
    {
        bool isAdmin = currentUserRoles!.Contains(ReservationsOperationClaims.Admin)
                   || currentUserRoles!.Contains(AuthOperationClaims.GeneralAdmin)
                   || currentUserRoles!.Contains(ReservationsOperationClaims.Employee) ? true : false;

        if (isAdmin)
        {
           return await _reservationRepository.GetListAsync(
            index: pageIndex,
            size: pageSize,
            cancellationToken: cancellationToken,
            include : r => r.Include(r => r.Member)!.Include(i => i.Item));
        }
        else
        {
            Member? member = await _memberService.GetAsync(m => m.UserId.Equals(userId));
            return  await _reservationRepository.GetListAsync(
            index: pageIndex,
            size: pageSize,
            cancellationToken: cancellationToken,
            predicate: c => c.MemberId == member!.Id, include: c => c.Include(c => c.Item)!);
        }
    }
}