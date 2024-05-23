using Application.Features.Auth.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FineRepository : EfRepositoryBase<Fine, Guid, BaseDbContext>, IFineRepository
{
    public FineRepository(BaseDbContext context) : base(context)
    {
    }



}