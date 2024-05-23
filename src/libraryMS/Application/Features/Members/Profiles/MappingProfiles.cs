using Application.Features.Members.Commands.Create;
using Application.Features.Members.Commands.Delete;
using Application.Features.Members.Commands.Update;
using Application.Features.Members.Queries.GetById;
using Application.Features.Members.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Items.Queries.GetDynamic;

namespace Application.Features.Members.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Member, CreateMemberCommand>().ReverseMap();
        CreateMap<Member, CreatedMemberResponse>().ReverseMap();
        CreateMap<Member, UpdateMemberCommand>().ReverseMap();
        CreateMap<Member, UpdatedMemberResponse>().ReverseMap();
        CreateMap<Member, DeleteMemberCommand>().ReverseMap();
        CreateMap<Member, DeletedMemberResponse>().ReverseMap();
        CreateMap<Member, GetByIdMemberResponse>().ReverseMap();
        CreateMap<Member, GetListMemberItemDto>().ReverseMap();
        CreateMap<IPaginate<Member>, GetListResponse<GetListMemberItemDto>>().ReverseMap();

        CreateMap<Member, GetDynamicMemberItemDto>().ReverseMap();

        CreateMap<IPaginate<Member>, GetListResponse<GetDynamicMemberItemDto>>().ReverseMap();

        
    }
}