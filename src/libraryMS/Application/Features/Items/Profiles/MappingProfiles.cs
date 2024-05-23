using Application.Features.Items.Commands.Create;
using Application.Features.Items.Commands.Delete;
using Application.Features.Items.Commands.Update;
using Application.Features.Items.Queries.GetById;
using Application.Features.Items.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Items.Queries.GetDynamic;

namespace Application.Features.Items.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Item, CreateItemCommand>().ReverseMap();
        CreateMap<Item, CreatedItemResponse>().ReverseMap();
        CreateMap<Item, UpdateItemCommand>().ReverseMap();
        CreateMap<Item, UpdatedItemResponse>().ReverseMap();
        CreateMap<Item, DeleteItemCommand>().ReverseMap();
        CreateMap<Item, DeletedItemResponse>().ReverseMap();
        CreateMap<Item, GetByIdItemResponse>().ReverseMap();
        CreateMap<Item, GetListItemListItemDto>().ReverseMap();
        CreateMap<Item, GetDynamicItemDto>()
        .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher!.Name))
        .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Location!.Section))
        .ForMember(dest => dest.FloorNumber, opt => opt.MapFrom(src => src.Location!.FloorNumber))
        .ForMember(dest => dest.ShelfNumber, opt => opt.MapFrom(src => src.Location!.ShelfNumber))
        .ForMember(dest => dest.LibraryName, opt => opt.MapFrom(src => src.Library!.Name));
        CreateMap<IPaginate<Item>, GetListResponse<GetDynamicItemDto>>().ReverseMap();
        CreateMap<IPaginate<Item>, GetListResponse<GetListItemListItemDto>>().ReverseMap();
    }
}