using Application.Features.Items.Constants;
using Application.Features.Items.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Items.Constants.ItemsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Items.Queries.GetById;

public class GetByIdItemQuery : IRequest<GetByIdItemResponse>
{
    public Guid Id { get; set; }


    public class GetByIdItemQueryHandler : IRequestHandler<GetByIdItemQuery, GetByIdItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly ItemBusinessRules _itemBusinessRules;

        public GetByIdItemQueryHandler(IMapper mapper, IItemRepository itemRepository, ItemBusinessRules itemBusinessRules)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _itemBusinessRules = itemBusinessRules;
        }

        public async Task<GetByIdItemResponse> Handle(GetByIdItemQuery request, CancellationToken cancellationToken)
        {
            Item? item = await _itemRepository.GetAsync(
                predicate: i => i.Id == request.Id, 
                cancellationToken: cancellationToken,
                include : i=>i.Include(i=>i.Library).Include(i=>i.Location).Include(i=>i.Publisher)!);
            await _itemBusinessRules.ItemShouldExistWhenSelected(item);

            GetByIdItemResponse response = _mapper.Map<GetByIdItemResponse>(item);
            return response;
        }
    }
}