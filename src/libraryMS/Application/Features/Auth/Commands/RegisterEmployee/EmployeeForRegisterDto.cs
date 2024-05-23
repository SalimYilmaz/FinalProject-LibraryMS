using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Auth.Commands.RegisterEmployee;

public class EmployeeForRegisterDto : IDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string NationalIdentity { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Position { get; set; }
    public Guid LibraryId { get; set; }
}