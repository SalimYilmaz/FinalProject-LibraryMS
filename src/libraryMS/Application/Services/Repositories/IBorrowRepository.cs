using Application.Features.Auth.Dtos;
using Application.Features.Fines.Queries.GetList;
using Application.Services.Fines;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBorrowRepository : IAsyncRepository<Borrow, Guid>, IRepository<Borrow, Guid>
{

}