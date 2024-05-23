using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reservations.Commands.Update;

public class UpdatedReservationResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime ReservationDate { get; set; }
}