using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ReservationRepository : EfRepositoryBase<Reservation, Guid, BaseDbContext>, IReservationRepository
{
    public ReservationRepository(BaseDbContext context) : base(context)
    {
    }
}