using NArchitecture.Core.Application.Responses;

namespace Application.Features.ItemAuthors.Commands.Delete;

public class DeletedItemAuthorResponse : IResponse
{
    public Guid Id { get; set; }
}