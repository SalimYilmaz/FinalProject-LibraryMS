using NArchitecture.Core.Application.Responses;

namespace Application.Features.Fines.Commands.Create;

public class CreatedFineResponse : IResponse
{
    public decimal FineAmount { get; set; }
    public string FineType { get; set; }
    public string Description { get; set; }
    public bool IsPaid { get; set; }
    public DateTime CreatedDate { get; set; }
}