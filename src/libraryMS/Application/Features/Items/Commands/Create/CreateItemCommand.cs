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
using Application.Features.ItemAuthors.Constants;
using System.Net.Mail;
using System.Net;
using Nest;

namespace Application.Features.Items.Commands.Create;

public class CreateItemCommand : MediatR.IRequest<CreatedItemResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
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
    public Guid LibraryId { get; set; }
    public int? InStock { get; set; }

    public string[] Roles => [Admin, Write, ItemsOperationClaims.Create, ItemsOperationClaims.Employee];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetItems"];

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreatedItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public CreateItemCommandHandler(IMapper mapper, IItemRepository itemRepository,
                                         ItemBusinessRules itemBusinessRules)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<CreatedItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Item item = _mapper.Map<Item>(request);

            Item createdItem = await _itemRepository.AddAsync(item);

            CreatedItemResponse response = _mapper.Map<CreatedItemResponse>(createdItem);
            return response;
        }
    }
}