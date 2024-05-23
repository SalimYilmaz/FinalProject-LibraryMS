using NArchitecture.Core.Application.Responses;

namespace Application.Features.Libraries.Commands.Update;

public class UpdatedLibraryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Website { get; set; }
}