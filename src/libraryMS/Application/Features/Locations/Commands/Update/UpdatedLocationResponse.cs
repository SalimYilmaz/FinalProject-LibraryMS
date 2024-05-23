using NArchitecture.Core.Application.Responses;

namespace Application.Features.Locations.Commands.Update;

public class UpdatedLocationResponse : IResponse
{
    public Guid Id { get; set; }
    public string Section { get; set; }
    public string FloorNumber { get; set; }
    public string ShelfNumber { get; set; }
}