using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.Users
{
    /// <summary>
    /// Represents the unique identifier for a user.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// </remarks>
    /// <param name="value">The value of the user identifier.</param>
    public class UserId(Guid value) : TypedIdValueBase(value)
    {
    }
}