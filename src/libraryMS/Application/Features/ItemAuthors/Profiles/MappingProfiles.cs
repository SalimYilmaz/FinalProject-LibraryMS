using Application.Features.ItemAuthors.Commands.Create;
using Application.Features.ItemAuthors.Commands.Delete;
using Application.Features.ItemAuthors.Commands.Update;
using Application.Features.ItemAuthors.Queries.GetById;
using Application.Features.ItemAuthors.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.ItemAuthors.Queries.GetDynamic;

namespace Application.Features.ItemAuthors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ItemAuthor, CreateItemAuthorCommand>().ReverseMap();
        CreateMap<ItemAuthor, CreatedItemAuthorResponse>().ReverseMap();
        CreateMap<ItemAuthor, UpdateItemAuthorCommand>().ReverseMap();
        CreateMap<ItemAuthor, UpdatedItemAuthorResponse>().ReverseMap();
        CreateMap<ItemAuthor, DeleteItemAuthorCommand>().ReverseMap();
        CreateMap<ItemAuthor, DeletedItemAuthorResponse>().ReverseMap();
        CreateMap<ItemAuthor, GetByIdItemAuthorResponse>().ReverseMap();
        CreateMap<ItemAuthor, GetListItemAuthorListItemDto>().ReverseMap();
        CreateMap<ItemAuthor, GetDynamicItemAuthorDto>().ReverseMap();
        CreateMap<IPaginate<ItemAuthor>, GetListResponse<GetListItemAuthorListItemDto>>().ReverseMap();
        CreateMap<IPaginate<ItemAuthor>, GetListResponse<GetDynamicItemAuthorDto>>().ReverseMap();
        
    }
}