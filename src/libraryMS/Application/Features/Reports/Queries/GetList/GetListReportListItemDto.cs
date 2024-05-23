using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Reports.Queries.GetList;

public class GetListReportListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public string ReportTitle { get; set; }
    public DateTime ReportDate { get; set; }
    public string ReportContent { get; set; }
    public string Status { get; set; }
}