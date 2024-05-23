using Application.Features.Fines.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Microsoft.Extensions.Hosting;
using Application.Features.Items.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Application.Features.Fines.Commands.Create;
using AutoMapper;
using Application.Features.Auth.Dtos;
using Nest;

namespace Application.Services.Fines;

public class FineManager : IFineService
{
    private readonly IFineRepository _fineRepository;

    public FineManager(IFineRepository fineRepository)
    {
        _fineRepository = fineRepository;
    }

    public async Task<Fine?> GetAsync(
        Expression<Func<Fine, bool>> predicate,
        Func<IQueryable<Fine>, IIncludableQueryable<Fine, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Fine? fine = await _fineRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return fine;
    }

    public async Task<IPaginate<Fine>?> GetListAsync(
        Expression<Func<Fine, bool>>? predicate = null,
        Func<IQueryable<Fine>, IOrderedQueryable<Fine>>? orderBy = null,
        Func<IQueryable<Fine>, IIncludableQueryable<Fine, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Fine> fineList = await _fineRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return fineList;
    }

    public async Task<Fine> AddAsync(Fine fine)
    {
        Fine addedFine = await _fineRepository.AddAsync(fine);

        return addedFine;
    }

    public async Task<Fine> UpdateAsync(Fine fine)
    {
        Fine updatedFine = await _fineRepository.UpdateAsync(fine);

        return updatedFine;
    }

    public async Task<Fine> DeleteAsync(Fine fine, bool permanent = false)
    {
        Fine deletedFine = await _fineRepository.DeleteAsync(fine);

        return deletedFine;
    }



}
