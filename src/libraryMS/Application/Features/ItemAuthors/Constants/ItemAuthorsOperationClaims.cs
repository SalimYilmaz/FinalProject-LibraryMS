

namespace Application.Features.ItemAuthors.Constants;


public static class ItemAuthorsOperationClaims
{
    private const string _section = "ItemAuthors";

    public const string Admin = $"{_section}.Admin";

    public const string WriteAll = "Write";
    public const string ReadAll = "Read";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
    public const string Employee = "Employee";
    public const string Member = "Member";
}