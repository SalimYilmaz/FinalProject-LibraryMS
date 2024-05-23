using Application.Features.Reviews.Commands.Create;
using Application.Features.Reviews.Commands.Delete;
using Application.Features.Reviews.Commands.Update;
using Application.Features.Reviews.Queries.GetById;
using Application.Features.Reviews.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Reviews.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Review, CreateReviewCommand>().ReverseMap();
        CreateMap<Review, CreatedReviewResponse>().ReverseMap();
        CreateMap<Review, UpdateReviewCommand>().ReverseMap();
        CreateMap<Review, UpdatedReviewResponse>().ReverseMap();
        CreateMap<Review, DeleteReviewCommand>().ReverseMap();
        CreateMap<Review, DeletedReviewResponse>().ReverseMap();
        CreateMap<Review, GetByIdReviewResponse>().ReverseMap();
        CreateMap<Review, GetListReviewListItemDto>().ReverseMap();
        CreateMap<IPaginate<Review>, GetListResponse<GetListReviewListItemDto>>().ReverseMap();
    }
}