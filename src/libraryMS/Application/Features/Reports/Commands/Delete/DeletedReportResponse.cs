using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reports.Commands.Delete;

public class DeletedReportResponse : IResponse
{
    public Guid Id { get; set; }
}