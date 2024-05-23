using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Items.Queries.GetList;

public class GetListItemListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Isbn { get; set; }
    public string Language { get; set; }
}