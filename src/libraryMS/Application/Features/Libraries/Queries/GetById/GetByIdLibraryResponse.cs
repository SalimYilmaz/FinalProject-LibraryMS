using NArchitecture.Core.Application.Responses;

namespace Application.Features.Libraries.Queries.GetById;

public class GetByIdLibraryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Website { get; set; }
}