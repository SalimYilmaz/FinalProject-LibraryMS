using Application.Features.Employees.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Employees.Rules;

public class EmployeeBusinessRules : BaseBusinessRules
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILocalizationService _localizationService;

    public EmployeeBusinessRules(IEmployeeRepository employeeRepository, ILocalizationService localizationService)
    {
        _employeeRepository = employeeRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, EmployeesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task EmployeeShouldExistWhenSelected(Employee? employee)
    {
        if (employee == null)
            await throwBusinessException(EmployeesBusinessMessages.EmployeeNotExists);
    }

    public async Task CheckIfNationalIdentityExists(string nationalId, CancellationToken cancellationToken)
    {
        Employee? nationalIdExist = await _employeeRepository.GetAsync(
            predicate: e => e.NationalIdentity.Equals(nationalId), enableTracking: false, cancellationToken: cancellationToken);
        if (nationalIdExist is not null)
            await throwBusinessException(EmployeesBusinessMessages.NationalIdAlreadyExists);
    }

    public async Task CheckIfPhoneNumberExists(string phone, CancellationToken cancellation)
    {
        Employee? phoneNumberExist = await _employeeRepository.GetAsync(
            predicate: e => e.PhoneNumber.Equals(phone), enableTracking: false, cancellationToken: cancellation);
        if (phoneNumberExist is not null)
            await throwBusinessException(EmployeesBusinessMessages.PhoneNumberAlreadyExists);
    }

    //public async Task CheckIfEmailExists(string email, CancellationToken cancellationToken)
    //{
    //    Employee? emailExist = await _employeeRepository.GetAsync(
    //        predicate: e => e.Email.Equals(email),
    //        enableTracking: false,
    //        cancellationToken: cancellationToken);
    //    if (emailExist is not null)
    //        await throwBusinessException(EmployeesBusinessMessages.EmailAlreadyExists);
    //}

    public async Task EmployeeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Employee? employee = await _employeeRepository.GetAsync(
            predicate: e => e.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await EmployeeShouldExistWhenSelected(employee);
    }
}