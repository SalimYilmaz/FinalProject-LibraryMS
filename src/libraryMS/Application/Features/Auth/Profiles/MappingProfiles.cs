using Application.Features.Auth.Commands.RegisterEmployee;
using Application.Features.Auth.Commands.RegisterMember;
using Application.Features.Auth.Commands.RevokeToken;
using Application.Features.Auth.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<NArchitecture.Core.Security.Entities.RefreshToken<Guid,Guid>, RefreshToken>().ReverseMap();
        CreateMap<RefreshToken, RevokedTokenResponse>().ReverseMap();
        CreateMap<Member,MemberForRegisterDto>().ReverseMap();

        CreateMap<Employee, EmployeeForRegisterDto>().ReverseMap();
    }
}
