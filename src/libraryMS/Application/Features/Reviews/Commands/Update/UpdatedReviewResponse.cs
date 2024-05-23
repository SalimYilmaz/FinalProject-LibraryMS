using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reviews.Commands.Update;

public class UpdatedReviewResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public string ReviewTitle { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewContent { get; set; }
}