using Application.Features.Items.Constants;
using Application.Features.Items.Constants;
using Application.Features.Items.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Items.Constants.ItemsOperationClaims;

namespace Application.Features.Items.Commands.Delete;

public class DeleteItemCommand : IRequest<DeletedItemResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ItemsOperationClaims.Delete,ItemsOperationClaims.Member, ItemsOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetItems"];

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, DeletedItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly ItemBusinessRules _itemBusinessRules;

        public DeleteItemCommandHandler(IMapper mapper, IItemRepository itemRepository,
                                         ItemBusinessRules itemBusinessRules)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _itemBusinessRules = itemBusinessRules;
        }

        public async Task<DeletedItemResponse> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            Item? item = await _itemRepository.GetAsync(predicate: i => i.Id == request.Id, cancellationToken: cancellationToken);
            await _itemBusinessRules.ItemShouldExistWhenSelected(item);

            await _itemRepository.DeleteAsync(item!);

            DeletedItemResponse response = _mapper.Map<DeletedItemResponse>(item);
            return response;
        }
    }
}