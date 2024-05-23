using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reviews.Commands.Delete;

public class DeletedReviewResponse : IResponse
{
    public Guid Id { get; set; }
}