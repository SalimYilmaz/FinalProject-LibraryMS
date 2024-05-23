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

namespace Application.Features.Items.Commands.Update;

public class UpdateItemCommand : IRequest<UpdatedItemResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Isbn { get; set; }
    public string Category { get; set; }
    public string Genre { get; set; }
    public DateTime PublicationDate { get; set; }
    public short TotalPages { get; set; }
    public string Language { get; set; }
    public string? Description { get; set; }
    public Guid PublisherId { get; set; }
    public Guid LocationId { get; set; }
    public Guid LibraryId{ get; set; }
    public int InStock { get; set; }

    public string[] Roles => [Admin, Write, ItemsOperationClaims.Update, ItemsOperationClaims.Member, ItemsOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetItems"];

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, UpdatedItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly ItemBusinessRules _itemBusinessRules;

        public UpdateItemCommandHandler(IMapper mapper, IItemRepository itemRepository,
                                         ItemBusinessRules itemBusinessRules)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _itemBusinessRules = itemBusinessRules;
        }

        public async Task<UpdatedItemResponse> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            Item? item = await _itemRepository.GetAsync(predicate: i => i.Id == request.Id, cancellationToken: cancellationToken);
            await _itemBusinessRules.ItemShouldExistWhenSelected(item);
            item = _mapper.Map(request, item);

            await _itemRepository.UpdateAsync(item!);

            UpdatedItemResponse response = _mapper.Map<UpdatedItemResponse>(item);
            return response;
        }
    }
}