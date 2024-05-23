using Application.Features.Items.Queries.GetDynamic;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Locations.Queries.GetDynamic;
public class GetDynamicLocationQuery : MediatR.IRequest<GetListResponse<GetDynamicLocationItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } 
    public DynamicQuery Dynamic { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => $"GetDynamicLocations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetLocations";
    public TimeSpan? SlidingExpiration { get; }
    public class GetDynamicLocationItemQueryHandler : IRequestHandler<GetDynamicLocationQuery, GetListResponse<GetDynamicLocationItemDto>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public GetDynamicLocationItemQueryHandler(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetDynamicLocationItemDto>> Handle(GetDynamicLocationQuery request, CancellationToken cancellationToken)
        {
            var items = await _locationRepository.GetListByDynamicAsync
                (request.Dynamic, index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize);

            GetListResponse<GetDynamicLocationItemDto> response = _mapper.Map<GetListResponse<GetDynamicLocationItemDto>>(items);

            return response;
        }
    }
}

