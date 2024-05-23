using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Features.Auth.Dtos;

namespace Application.Services.Fines;

public interface IFineService
{
    Task<Fine?> GetAsync(
        Expression<Func<Fine, bool>> predicate,
        Func<IQueryable<Fine>, IIncludableQueryable<Fine, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Fine>?> GetListAsync(
        Expression<Func<Fine, bool>>? predicate = null,
        Func<IQueryable<Fine>, IOrderedQueryable<Fine>>? orderBy = null,
        Func<IQueryable<Fine>, IIncludableQueryable<Fine, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Fine> AddAsync(Fine fine);
    Task<Fine> UpdateAsync(Fine fine);
    Task<Fine> DeleteAsync(Fine fine, bool permanent = false);


}
