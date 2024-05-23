using NArchitecture.Core.Application.Responses;

namespace Application.Features.ItemAuthors.Queries.GetById;

public class GetByIdItemAuthorResponse : IResponse
{
    public Guid Id { get; set; }
}