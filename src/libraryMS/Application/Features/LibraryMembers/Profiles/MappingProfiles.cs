using Application.Features.LibraryMembers.Commands.Create;
using Application.Features.LibraryMembers.Commands.Delete;
using Application.Features.LibraryMembers.Commands.Update;
using Application.Features.LibraryMembers.Queries.GetById;
using Application.Features.LibraryMembers.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.LibraryMembers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LibraryMember, CreateLibraryMemberCommand>().ReverseMap();
        CreateMap<LibraryMember, CreatedLibraryMemberResponse>().ReverseMap();
        CreateMap<LibraryMember, UpdateLibraryMemberCommand>().ReverseMap();
        CreateMap<LibraryMember, UpdatedLibraryMemberResponse>().ReverseMap();
        CreateMap<LibraryMember, DeleteLibraryMemberCommand>().ReverseMap();
        CreateMap<LibraryMember, DeletedLibraryMemberResponse>().ReverseMap();
        CreateMap<LibraryMember, GetByIdLibraryMemberResponse>().ReverseMap();
        CreateMap<LibraryMember, GetListLibraryMemberListItemDto>().ReverseMap();
        CreateMap<IPaginate<LibraryMember>, GetListResponse<GetListLibraryMemberListItemDto>>().ReverseMap();
    }
}