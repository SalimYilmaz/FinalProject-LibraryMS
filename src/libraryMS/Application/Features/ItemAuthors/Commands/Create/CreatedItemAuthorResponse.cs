using NArchitecture.Core.Application.Responses;

namespace Application.Features.ItemAuthors.Commands.Create;

public class CreatedItemAuthorResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid? ItemId { get; set; }
    public Guid AuthorId { get; set; }
}