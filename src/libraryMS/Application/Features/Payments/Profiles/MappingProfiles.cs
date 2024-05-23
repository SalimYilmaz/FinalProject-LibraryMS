using Application.Features.Payments.Commands.Create;
using Application.Features.Payments.Commands.Delete;
using Application.Features.Payments.Commands.Update;
using Application.Features.Payments.Queries.GetById;
using Application.Features.Payments.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Payments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Payment, CreatePaymentCommand>().ReverseMap();
        CreateMap<Payment, CreatedPaymentResponse>().ReverseMap();
        CreateMap<Payment, UpdatePaymentCommand>().ReverseMap();
        CreateMap<Payment, UpdatedPaymentResponse>().ReverseMap();
        CreateMap<Payment, DeletePaymentCommand>().ReverseMap();
        CreateMap<Payment, DeletedPaymentResponse>().ReverseMap();
        CreateMap<Payment, GetByIdPaymentResponse>().ReverseMap();
        CreateMap<Payment, GetListPaymentListItemDto>().ReverseMap();
        CreateMap<IPaginate<Payment>, GetListResponse<GetListPaymentListItemDto>>().ReverseMap();
    }
}