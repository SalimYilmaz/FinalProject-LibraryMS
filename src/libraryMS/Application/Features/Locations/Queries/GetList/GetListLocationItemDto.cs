using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Locations.Queries.GetList;

public class GetListLocationItemDto : IDto
{
    public Guid Id { get; set; }
    public string Section { get; set; }
    public string FloorNumber { get; set; }
    public string ShelfNumber { get; set; }
}