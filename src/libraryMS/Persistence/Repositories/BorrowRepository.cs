using Application.Features.Auth.Dtos;
using Application.Features.Fines.Queries.GetList;
using Application.Features.Items.Rules;
using Application.Services.Fines;
using Application.Services.Items;
using Application.Services.Members;
using Application.Services.Repositories;
using Application.Services.UsersService;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BorrowRepository : EfRepositoryBase<Borrow, Guid, BaseDbContext>, IBorrowRepository
{
    public BorrowRepository(BaseDbContext context) : base(context)
    {
    }
}