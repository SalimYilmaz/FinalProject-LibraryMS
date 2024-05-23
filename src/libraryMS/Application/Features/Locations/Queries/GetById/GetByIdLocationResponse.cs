using NArchitecture.Core.Application.Responses;

namespace Application.Features.Locations.Queries.GetById;

public class GetByIdLocationResponse : IResponse
{
    public Guid Id { get; set; }
    public string Section { get; set; }
    public string FloorNumber { get; set; }
    public string ShelfNumber { get; set; }
}