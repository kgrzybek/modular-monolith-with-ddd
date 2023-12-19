using CSharpFunctionalExtensions;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;

public class UserRole : ValueObject
{
    public static UserRole Member => new UserRole(nameof(Member));

    public static UserRole Administrator => new UserRole(nameof(Administrator));

    public string Value { get; }

    private UserRole(string value)
    {
        Value = value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}