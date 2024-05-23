using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reports.Commands.Update;

public class UpdatedReportResponse : IResponse
{
    public string ReportTitle { get; set; }
    public DateTime ReportDate { get; set; }
    public string ReportContent { get; set; }
    public string Status { get; set; }
}