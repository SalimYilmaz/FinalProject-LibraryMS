using NArchitecture.Core.Application.Responses;

namespace Application.Features.Items.Commands.Create;

public class CreatedItemResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Isbn { get; set; }
    public string Category { get; set; }
    public string Genre { get; set; }
    public DateTime PublicationDate { get; set; }
    public short TotalPages { get; set; }
    public string Language { get; set; }
    public string? Description { get; set; }
    public Guid PublisherId { get; set; }
    public Guid LocationId { get; set; }
}