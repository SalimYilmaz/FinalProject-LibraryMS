using Application.Features.ItemAuthors.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ItemAuthors.Rules;

public class ItemAuthorBusinessRules : BaseBusinessRules
{
    private readonly IItemAuthorRepository _itemAuthorRepository;
    private readonly ILocalizationService _localizationService;

    public ItemAuthorBusinessRules(IItemAuthorRepository itemAuthorRepository, ILocalizationService localizationService)
    {
        _itemAuthorRepository = itemAuthorRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ItemAuthorsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ItemAuthorShouldExistWhenSelected(ItemAuthor? itemAuthor)
    {
        if (itemAuthor == null)
            await throwBusinessException(ItemAuthorsBusinessMessages.ItemAuthorNotExists);
    }

    public async Task ItemAuthorIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ItemAuthor? itemAuthor = await _itemAuthorRepository.GetAsync(
            predicate: ia => ia.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ItemAuthorShouldExistWhenSelected(itemAuthor);
    }
}