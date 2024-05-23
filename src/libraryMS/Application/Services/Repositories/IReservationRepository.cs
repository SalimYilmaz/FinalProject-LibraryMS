using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IReservationRepository : IAsyncRepository<Reservation, Guid>, IRepository<Reservation, Guid>
{
}