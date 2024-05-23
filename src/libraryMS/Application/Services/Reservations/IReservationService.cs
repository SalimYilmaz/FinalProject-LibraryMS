using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Reservations;

public interface IReservationService
{
    Task<Reservation?> GetAsync(
        Expression<Func<Reservation, bool>> predicate,
        Func<IQueryable<Reservation>, IIncludableQueryable<Reservation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Reservation>?> GetListAsync(
        Expression<Func<Reservation, bool>>? predicate = null,
        Func<IQueryable<Reservation>, IOrderedQueryable<Reservation>>? orderBy = null,
        Func<IQueryable<Reservation>, IIncludableQueryable<Reservation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Reservation> AddAsync(Reservation reservation);
    Task<Reservation> UpdateAsync(Reservation reservation);
    Task<Reservation> DeleteAsync(Reservation reservation, bool permanent = false);
}
