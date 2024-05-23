using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reservations.Commands.Delete;

public class DeletedReservationResponse : IResponse
{
    public Guid Id { get; set; }
}