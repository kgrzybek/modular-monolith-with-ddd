using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.Members
{
    /// <summary>
    /// Represents the unique identifier for a member.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MemberId"/> class.
    /// </remarks>
    /// <param name="value">The value of the member identifier.</param>
    public class MemberId(Guid value) : TypedIdValueBase(value)
    {
    }
}