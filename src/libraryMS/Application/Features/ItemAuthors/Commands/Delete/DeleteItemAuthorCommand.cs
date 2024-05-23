using Application.Features.ItemAuthors.Constants;
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

namespace Application.Features.ItemAuthors.Commands.Delete;

public class DeleteItemAuthorCommand : IRequest<DeletedItemAuthorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ItemAuthorsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetItemAuthors"];

    public class DeleteItemAuthorCommandHandler : IRequestHandler<DeleteItemAuthorCommand, DeletedItemAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IItemAuthorRepository _itemAuthorRepository;
        private readonly ItemAuthorBusinessRules _itemAuthorBusinessRules;

        public DeleteItemAuthorCommandHandler(IMapper mapper, IItemAuthorRepository itemAuthorRepository,
                                         ItemAuthorBusinessRules itemAuthorBusinessRules)
        {
            _mapper = mapper;
            _itemAuthorRepository = itemAuthorRepository;
            _itemAuthorBusinessRules = itemAuthorBusinessRules;
        }

        public async Task<DeletedItemAuthorResponse> Handle(DeleteItemAuthorCommand request, CancellationToken cancellationToken)
        {
            ItemAuthor? itemAuthor = await _itemAuthorRepository.GetAsync(predicate: ia => ia.Id == request.Id, cancellationToken: cancellationToken);
            await _itemAuthorBusinessRules.ItemAuthorShouldExistWhenSelected(itemAuthor);

            await _itemAuthorRepository.DeleteAsync(itemAuthor!);

            DeletedItemAuthorResponse response = _mapper.Map<DeletedItemAuthorResponse>(itemAuthor);
            return response;
        }
    }
}