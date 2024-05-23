using Application.Features.Items.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using YamlDotNet.Core;
using Nest;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System.Diagnostics.CodeAnalysis;

namespace Application.Features.Items.Rules;

public class ItemBusinessRules : BaseBusinessRules
{
    private readonly IItemRepository _itemRepository;
    private readonly ILocalizationService _localizationService;

    public ItemBusinessRules(IItemRepository itemRepository, ILocalizationService localizationService)
    {
        _itemRepository = itemRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ItemsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ItemShouldExistWhenSelected(Item? item)
    {
        if (item == null)
            await throwBusinessException(ItemsBusinessMessages.ItemNotExists);
    }


    public async Task ItemIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Item? item = await _itemRepository.GetAsync(
            predicate: i => i.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ItemShouldExistWhenSelected(item);
    }
    public async Task CheckIfItemStockIsLessThanZero(Item item)
    {
        if(item.InStock < 0)
        {
            await throwBusinessException(ItemsBusinessMessages.ItemStockIsLessThanZero);
        }
    }
}