using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.Members.Events
{
    /// <summary>
    /// Represents a domain event that is raised when a member is created.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MemberCreatedDomainEvent"/> class.
    /// </remarks>
    /// <param name="memberId">The ID of the member.</param>
    public class MemberCreatedDomainEvent(MemberId memberId) : DomainEventBase
    {
        /// <summary>
        /// Gets the ID of the member.
        /// </summary>
        public MemberId MemberId { get; } = memberId;
    }
}