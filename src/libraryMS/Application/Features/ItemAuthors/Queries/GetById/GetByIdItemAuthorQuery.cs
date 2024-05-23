using Application.Features.ItemAuthors.Constants;
using Application.Features.ItemAuthors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ItemAuthors.Constants.ItemAuthorsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ItemAuthors.Queries.GetById;

public class GetByIdItemAuthorQuery : IRequest<GetByIdItemAuthorResponse>
{
    public Guid Id { get; set; }

    public class GetByIdItemAuthorQueryHandler : IRequestHandler<GetByIdItemAuthorQuery, GetByIdItemAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IItemAuthorRepository _itemAuthorRepository;
        private readonly ItemAuthorBusinessRules _itemAuthorBusinessRules;

        public GetByIdItemAuthorQueryHandler(IMapper mapper, IItemAuthorRepository itemAuthorRepository, ItemAuthorBusinessRules itemAuthorBusinessRules)
        {
            _mapper = mapper;
            _itemAuthorRepository = itemAuthorRepository;
            _itemAuthorBusinessRules = itemAuthorBusinessRules;
        }

        public async Task<GetByIdItemAuthorResponse> Handle(GetByIdItemAuthorQuery request, CancellationToken cancellationToken)
        {
            ItemAuthor? itemAuthor = await _itemAuthorRepository.GetAsync(
                predicate: ia => ia.Id == request.Id, 
                cancellationToken: cancellationToken,
                include : ia => ia.Include(ia=>ia.Item)!.Include(ia=>ia.Author)!);
            
            await _itemAuthorBusinessRules.ItemAuthorShouldExistWhenSelected(itemAuthor);

            GetByIdItemAuthorResponse response = _mapper.Map<GetByIdItemAuthorResponse>(itemAuthor);
            return response;
        }
    }
}