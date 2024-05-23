using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Fines.Queries.GetList;

public class GetListFineListItemDto : IDto
{
    public Guid Id { get; set; }
    public decimal FineAmount { get; set; }
    public DateTime CreatedDate { get; set; }
}