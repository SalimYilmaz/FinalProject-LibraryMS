using NArchitecture.Core.Application.Responses;

namespace Application.Features.ItemAuthors.Commands.Update;

public class UpdatedItemAuthorResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public Guid AuthorId { get; set; }
}