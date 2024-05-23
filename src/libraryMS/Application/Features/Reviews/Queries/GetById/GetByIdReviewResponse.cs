using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reviews.Queries.GetById;

public class GetByIdReviewResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public string ReviewTitle { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewContent { get; set; }
}