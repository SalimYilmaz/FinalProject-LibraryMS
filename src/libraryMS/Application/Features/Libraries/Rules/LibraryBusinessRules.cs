using Application.Features.Libraries.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Libraries.Rules;

public class LibraryBusinessRules : BaseBusinessRules
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly ILocalizationService _localizationService;

    public LibraryBusinessRules(ILibraryRepository libraryRepository, ILocalizationService localizationService)
    {
        _libraryRepository = libraryRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, LibrariesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task LibraryShouldExistWhenSelected(Library? library)
    {
        if (library == null)
            await throwBusinessException(LibrariesBusinessMessages.LibraryNotExists);
    }

    public async Task LibraryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Library? library = await _libraryRepository.GetAsync(
            predicate: l => l.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LibraryShouldExistWhenSelected(library);
    }

    public async Task CheckIfNameExists(string name, CancellationToken cancellationToken)
    {
        Library? libraryNameExist = await _libraryRepository.GetAsync(
            predicate: l => l.Name.Equals(name), enableTracking: false, cancellationToken: cancellationToken);
        if (libraryNameExist is not null)
            await throwBusinessException(LibrariesBusinessMessages.NameAlreadyExists);
    }

    public async Task CheckIfPhoneNumberExists(string phone, CancellationToken cancellationToken)
    {
        Library? libraryNumberExist = await _libraryRepository.GetAsync(
            predicate: l => l.PhoneNumber.Equals(phone), enableTracking: false, cancellationToken: cancellationToken);
        if (libraryNumberExist is not null)
            await throwBusinessException(LibrariesBusinessMessages.PhoneNumberAlreadyExists);
    }
}