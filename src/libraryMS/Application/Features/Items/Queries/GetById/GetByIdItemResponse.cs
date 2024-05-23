using NArchitecture.Core.Application.Responses;

namespace Application.Features.Items.Queries.GetById;

public class GetByIdItemResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string ISBN { get; set; }
    public string Category { get; set; }
    public string Genre { get; set; }
    public DateTime PublicationDate { get; set; }
    public short TotalPages { get; set; }
    public string Language { get; set; }
    public string? Description { get; set; }

    public string LibraryName { get; set; }
    public string LibraryAddress { get; set; }
    public string LibraryPhoneNumber { get; set; }
    public string LibraryCity { get; set; }
    public string LibraryWebsite { get; set; }
    public string LocationSection { get; set; }
    public string LocationFloorNumber { get; set; }
    public string LocationShelfNumber { get; set; }

    public string PublisherName { get; set; }
    public string PublisherPhoneNumber { get; set; }

}