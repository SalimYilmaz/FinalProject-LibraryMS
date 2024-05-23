using Application.Features.Reservations.Commands.Create;
using Application.Features.Reservations.Commands.Delete;
using Application.Features.Reservations.Commands.Update;
using Application.Features.Reservations.Queries.GetById;
using Application.Features.Reservations.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Reservations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Reservation, CreateReservationCommand>().ReverseMap();
        CreateMap<Reservation, CreatedReservationResponse>().ReverseMap();
        CreateMap<Reservation, UpdateReservationCommand>().ReverseMap();
        CreateMap<Reservation, UpdatedReservationResponse>().ReverseMap();
        CreateMap<Reservation, DeleteReservationCommand>().ReverseMap();
        CreateMap<Reservation, DeletedReservationResponse>().ReverseMap();
        CreateMap<Reservation, GetByIdReservationResponse>().ReverseMap();
        CreateMap<Reservation, GetListReservationListItemDto>().ReverseMap();
        CreateMap<IPaginate<Reservation>, GetListResponse<GetListReservationListItemDto>>().ReverseMap();
    }
}