using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Libraries.Queries.GetList;

public class GetListLibraryListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Website { get; set; }
}