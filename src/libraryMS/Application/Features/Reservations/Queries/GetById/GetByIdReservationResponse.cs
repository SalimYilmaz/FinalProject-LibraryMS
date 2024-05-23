using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reservations.Queries.GetById;

public class GetByIdReservationResponse : IResponse
{
    public DateTime ReservationDate { get; set; }
    public string MemberName{ get; set; }
    public string MemberSurname { get; set; }   
    public string ItemName{ get; set; }
    public string ItemType { get; set; }
}