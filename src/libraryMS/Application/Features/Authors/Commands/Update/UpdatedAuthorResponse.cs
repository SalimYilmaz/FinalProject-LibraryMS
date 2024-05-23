using NArchitecture.Core.Application.Responses;

namespace Application.Features.Authors.Commands.Update;

public class UpdatedAuthorResponse : IResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
}