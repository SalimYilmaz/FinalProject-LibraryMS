using Application.Features.Reservations.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Reservations;

public class ReservationManager : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ReservationBusinessRules _reservationBusinessRules;

    public ReservationManager(IReservationRepository reservationRepository, ReservationBusinessRules reservationBusinessRules)
    {
        _reservationRepository = reservationRepository;
        _reservationBusinessRules = reservationBusinessRules;
    }

    public async Task<Reservation?> GetAsync(
        Expression<Func<Reservation, bool>> predicate,
        Func<IQueryable<Reservation>, IIncludableQueryable<Reservation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Reservation? reservation = await _reservationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return reservation;
    }

    public async Task<IPaginate<Reservation>?> GetListAsync(
        Expression<Func<Reservation, bool>>? predicate = null,
        Func<IQueryable<Reservation>, IOrderedQueryable<Reservation>>? orderBy = null,
        Func<IQueryable<Reservation>, IIncludableQueryable<Reservation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Reservation> reservationList = await _reservationRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return reservationList;
    }

    public async Task<Reservation> AddAsync(Reservation reservation)
    {
        Reservation addedReservation = await _reservationRepository.AddAsync(reservation);

        return addedReservation;
    }

    public async Task<Reservation> UpdateAsync(Reservation reservation)
    {
        Reservation updatedReservation = await _reservationRepository.UpdateAsync(reservation);

        return updatedReservation;
    }

    public async Task<Reservation> DeleteAsync(Reservation reservation, bool permanent = false)
    {
        Reservation deletedReservation = await _reservationRepository.DeleteAsync(reservation);

        return deletedReservation;
    }
}
