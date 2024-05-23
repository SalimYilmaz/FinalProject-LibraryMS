using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Reservations.Queries.GetList;

public class GetListReservationListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }

    public string ItemName { get; set; }
}