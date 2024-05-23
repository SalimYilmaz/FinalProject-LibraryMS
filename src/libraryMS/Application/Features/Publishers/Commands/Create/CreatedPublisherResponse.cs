using NArchitecture.Core.Application.Responses;

namespace Application.Features.Publishers.Commands.Create;

public class CreatedPublisherResponse : IResponse
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}