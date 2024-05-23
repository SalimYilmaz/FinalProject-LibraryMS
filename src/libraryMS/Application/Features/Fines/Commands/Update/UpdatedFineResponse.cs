using NArchitecture.Core.Application.Responses;

namespace Application.Features.Fines.Commands.Update;

public class UpdatedFineResponse : IResponse
{
    public decimal FineAmount { get; set; }
    public string FineType { get; set; }
    public DateTime IssueDate { get; set; }
    public string Description { get; set; }
    public bool IsPaid { get; set; }
}