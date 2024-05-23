

namespace Application.Features.Borrows.Constants;

public static class BorrowsOperationClaims
{
    private const string _section = "Borrows";

    public const string Admin = $"{_section}.Admin";

    public const string WriteAll = "Write";
    public const string ReadAll = "Read";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
    public const string Member = "Member";
    public const string GeneralEmployee = "Employee";

}