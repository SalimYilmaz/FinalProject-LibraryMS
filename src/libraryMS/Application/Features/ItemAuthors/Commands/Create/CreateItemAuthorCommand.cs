using Application.Features.ItemAuthors.Constants;
using Application.Features.ItemAuthors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ItemAuthors.Constants.ItemAuthorsOperationClaims;

namespace Application.Features.ItemAuthors.Commands.Create;

public class CreateItemAuthorCommand : IRequest<CreatedItemAuthorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid? ItemId { get; set; }
    public Guid AuthorId { get; set; }

    public string[] Roles => [Admin, Write, ItemAuthorsOperationClaims.Create, ItemAuthorsOperationClaims.Employee, ItemAuthorsOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetItemAuthors"];

    public class CreateItemAuthorCommandHandler : IRequestHandler<CreateItemAuthorCommand, CreatedItemAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IItemAuthorRepository _itemAuthorRepository;
        private readonly ItemAuthorBusinessRules _itemAuthorBusinessRules;

        public CreateItemAuthorCommandHandler(IMapper mapper, IItemAuthorRepository itemAuthorRepository,
                                         ItemAuthorBusinessRules itemAuthorBusinessRules)
        {
            _mapper = mapper;
            _itemAuthorRepository = itemAuthorRepository;
            _itemAuthorBusinessRules = itemAuthorBusinessRules;
        }

        public async Task<CreatedItemAuthorResponse> Handle(CreateItemAuthorCommand request, CancellationToken cancellationToken)
        {
            ItemAuthor itemAuthor = _mapper.Map<ItemAuthor>(request);

            await _itemAuthorRepository.AddAsync(itemAuthor);

            CreatedItemAuthorResponse response = _mapper.Map<CreatedItemAuthorResponse>(itemAuthor);
            return response;
        }
    }
}