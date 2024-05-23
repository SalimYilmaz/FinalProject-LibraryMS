using Application.Features.LibraryMembers.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.LibraryMembers.Rules;

public class LibraryMemberBusinessRules : BaseBusinessRules
{
    private readonly ILibraryMemberRepository _libraryMemberRepository;
    private readonly ILocalizationService _localizationService;

    public LibraryMemberBusinessRules(ILibraryMemberRepository libraryMemberRepository, ILocalizationService localizationService)
    {
        _libraryMemberRepository = libraryMemberRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, LibraryMembersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task LibraryMemberShouldExistWhenSelected(LibraryMember? libraryMember)
    {
        if (libraryMember == null)
            await throwBusinessException(LibraryMembersBusinessMessages.LibraryMemberNotExists);
    }

    public async Task LibraryMemberIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LibraryMember? libraryMember = await _libraryMemberRepository.GetAsync(
            predicate: lm => lm.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LibraryMemberShouldExistWhenSelected(libraryMember);
    }
}