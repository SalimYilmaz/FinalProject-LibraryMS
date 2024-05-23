using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reports.Queries.GetById;

public class GetByIdReportResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public string ReportTitle { get; set; }
    public DateTime ReportDate { get; set; }
    public string ReportContent { get; set; }
    public string Status { get; set; }
}