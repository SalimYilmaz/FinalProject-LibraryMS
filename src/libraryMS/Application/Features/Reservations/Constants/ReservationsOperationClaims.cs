

namespace Application.Features.Reservations.Constants;


public static class ReservationsOperationClaims
{
    private const string _section = "Reservations";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
    public const string Employee = "Employee";
    public const string Member = "Member";
}