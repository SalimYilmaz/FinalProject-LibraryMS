using Application.Features.Borrows.Commands.Create;
using Application.Features.Borrows.Commands.Delete;
using Application.Features.Borrows.Commands.Update;
using Application.Features.Borrows.Queries.GetById;
using Application.Features.Borrows.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Fines.Commands.Create;
using Application.Features.Items.Queries.GetList;

namespace Application.Features.Borrows.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Borrow, CreateBorrowCommand>().ReverseMap();
        CreateMap<Borrow, CreateFineCommand>().ReverseMap();
        CreateMap<Borrow, CreatedBorrowResponse>().ReverseMap();
        CreateMap<Borrow, UpdateBorrowCommand>().ReverseMap();
        CreateMap<Borrow, UpdatedBorrowResponse>().ReverseMap();
        CreateMap<Borrow, DeleteBorrowCommand>().ReverseMap();
        CreateMap<Borrow, DeletedBorrowResponse>().ReverseMap();
        CreateMap<Borrow, GetListBorrowListItemDto>().ReverseMap();
        CreateMap<Borrow,GetByIdBorrowResponse>().ReverseMap();
        CreateMap<IPaginate<Borrow>, GetListResponse<GetListBorrowListItemDto>>().ReverseMap();
    }
}