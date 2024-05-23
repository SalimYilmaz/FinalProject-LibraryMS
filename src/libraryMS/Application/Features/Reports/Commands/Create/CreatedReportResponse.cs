using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reports.Commands.Create;

public class CreatedReportResponse : IResponse
{
    public string ReportTitle { get; set; }
    public DateTime ReportDate { get; set; }
    public string ReportContent { get; set; }
    public string Status { get; set; }
}