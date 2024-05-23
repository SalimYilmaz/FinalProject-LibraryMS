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

namespace Application.Features.ItemAuthors.Commands.Update;

public class UpdateItemAuthorCommand : IRequest<UpdatedItemAuthorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public Guid AuthorId { get; set; }

    public string[] Roles => [Admin, Write, ItemAuthorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetItemAuthors"];

    public class UpdateItemAuthorCommandHandler : IRequestHandler<UpdateItemAuthorCommand, UpdatedItemAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IItemAuthorRepository _itemAuthorRepository;
        private readonly ItemAuthorBusinessRules _itemAuthorBusinessRules;

        public UpdateItemAuthorCommandHandler(IMapper mapper, IItemAuthorRepository itemAuthorRepository,
                                         ItemAuthorBusinessRules itemAuthorBusinessRules)
        {
            _mapper = mapper;
            _itemAuthorRepository = itemAuthorRepository;
            _itemAuthorBusinessRules = itemAuthorBusinessRules;
        }

        public async Task<UpdatedItemAuthorResponse> Handle(UpdateItemAuthorCommand request, CancellationToken cancellationToken)
        {
            ItemAuthor? itemAuthor = await _itemAuthorRepository.GetAsync(predicate: ia => ia.Id == request.Id, cancellationToken: cancellationToken);
            await _itemAuthorBusinessRules.ItemAuthorShouldExistWhenSelected(itemAuthor);
            itemAuthor = _mapper.Map(request, itemAuthor);

            await _itemAuthorRepository.UpdateAsync(itemAuthor!);

            UpdatedItemAuthorResponse response = _mapper.Map<UpdatedItemAuthorResponse>(itemAuthor);
            return response;
        }
    }
}