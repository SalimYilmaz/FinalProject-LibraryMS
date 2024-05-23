using Application.Features.Auth.Dtos;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFineRepository : IAsyncRepository<Fine, Guid>, IRepository<Fine, Guid>
{
}