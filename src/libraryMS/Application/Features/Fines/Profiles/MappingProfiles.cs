using Application.Features.Fines.Commands.Create;
using Application.Features.Fines.Commands.Delete;
using Application.Features.Fines.Commands.Update;
using Application.Features.Fines.Queries.GetById;
using Application.Features.Fines.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Auth.Dtos;

namespace Application.Features.Fines.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Fine, CreateFineCommand>().ReverseMap();
        CreateMap<Fine, CreatedFineResponse>().ReverseMap();
        CreateMap<Fine, UpdateFineCommand>().ReverseMap();
        CreateMap<Fine, UpdatedFineResponse>().ReverseMap();
        CreateMap<Fine, DeleteFineCommand>().ReverseMap();
        CreateMap<Fine, DeletedFineResponse>().ReverseMap();
        CreateMap<Fine, GetByIdFineResponse>().ReverseMap();
        CreateMap<Fine, GetListFineListItemDto>().ReverseMap();
        CreateMap<IPaginate<Fine>, GetListResponse<GetListFineListItemDto>>().ReverseMap();
    }
}