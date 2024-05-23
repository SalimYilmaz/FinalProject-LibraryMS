using static Nest.JoinField;

namespace Application.Features.Reservations.Constants;

public static class ReservationsBusinessMessages
{
    public const string SectionName = "Reservations";

    public const string ReservationNotExists = "ReservationNotExists";
    public const string ItemNotExists = "ItemNotExists";
    public const string ReservationIsNotForAvailable = "ReservationIsNotForAvailable";
    public const string ReservationAlreadyExistsForToday = "ReservationAlreadyExistsForToday";
    public const string ReservationIntervalNotElapsed = "ReservationIntervalNotElapsed";
    public const string ReservationForbiddenMessage = "ReservationForbiddenMessage";
    public const string ReservationsCouldNotBeFound = "ReservationsCouldNotBeFound";
}