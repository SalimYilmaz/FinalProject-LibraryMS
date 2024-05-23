using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ItemAuthors.Queries.GetList;

public class GetListItemAuthorListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public Guid AuthorId { get; set; }
}