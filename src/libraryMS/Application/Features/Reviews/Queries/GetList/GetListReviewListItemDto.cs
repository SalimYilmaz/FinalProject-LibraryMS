using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Reviews.Queries.GetList;

public class GetListReviewListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public string ReviewTitle { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewContent { get; set; }
}