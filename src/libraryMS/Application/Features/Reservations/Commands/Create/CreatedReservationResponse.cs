using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reservations.Commands.Create;

public class CreatedReservationResponse : IResponse
{
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime ReservationDate { get; set; }
}