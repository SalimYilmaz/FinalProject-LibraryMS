using NArchitecture.Core.Application.Responses;

namespace Application.Features.Fines.Commands.Delete;

public class DeletedFineResponse : IResponse
{
    public Guid Id { get; set; }
}