using NArchitecture.Core.Application.Responses;

namespace Application.Features.Items.Commands.Delete;

public class DeletedItemResponse : IResponse
{
    public Guid Id { get; set; }
}