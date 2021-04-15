# Entity (DDD)

## Definition

*When an object is distinguished by its identity, rather than its attributes, make this primary to its definition in the model. Keep the class definition simple and focused on life cycle continuity and identity. Define a means of distinguishing each object regardless of its form or history.*

Source: [Domain-Driven Design: Tackling Complexity in the Heart of Software, Eric Evans](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215)

## Example

### Model

![](http://www.plantuml.com/plantuml/png/7OunieCm34JxVugV0nZrgIZ8GOob08DjCV9g67Bwu3IxM-oRUMD3D7Z9Vu-jfkmiRlb_1Oxs9B9u3ik6rMTlOaahf698McXVx7FDibDHzXmj5AsQxsiuUp0pbTWWHgofKOg8MPUWxm2nqkXLiU4AqpIH_6P7XgEBZ5BvxYy0)

### Code

```csharp

public class MeetingGroup : Entity, IAggregateRoot
{
    public MeetingGroupId Id { get; private set; }

    private string _name;

    private string _description;

    private MeetingGroupLocation _location;

    private MemberId _creatorId;

    private List<MeetingGroupMember> _members;

    private DateTime _createDate;

    private DateTime? _paymentDateTo;

    internal static MeetingGroup CreateBasedOnProposal(
        MeetingGroupProposalId meetingGroupProposalId,
        string name,
        string description,
        MeetingGroupLocation location,
        MemberId creatorId)
    {
        return new MeetingGroup(meetingGroupProposalId, name, description, location, creatorId);
    }

    private MeetingGroup()
    {
        // Only for EF.
    }

    private MeetingGroup(MeetingGroupProposalId meetingGroupProposalId, string name, string description, MeetingGroupLocation location, MemberId creatorId)
    {
        this.Id = new MeetingGroupId(meetingGroupProposalId.Value);
        this._name = name;
        this._description = description;
        this._creatorId = creatorId;
        this._location = location;
        this._createDate = SystemClock.Now;

        this.AddDomainEvent(new MeetingGroupCreatedDomainEvent(this.Id, creatorId));

        this._members = new List<MeetingGroupMember>();
        this._members.Add(MeetingGroupMember.CreateNew(this.Id, this._creatorId, MeetingGroupMemberRole.Organizer));
    }

    public void EditGeneralAttributes(string name, string description, MeetingGroupLocation location)
    {
        this._name = name;
        this._description = description;
        this._location = location;

        this.AddDomainEvent(new MeetingGroupGeneralAttributesEditedDomainEvent(this._name, this._description, this._location));
    }

    public void JoinToGroupMember(MemberId memberId)
    {
        this.CheckRule(new MeetingGroupMemberCannotBeAddedTwiceRule(_members, memberId));

        this._members.Add(MeetingGroupMember.CreateNew(this.Id, memberId, MeetingGroupMemberRole.Member));
    }

    public void LeaveGroup(MemberId memberId)
    {
        this.CheckRule(new NotActualGroupMemberCannotLeaveGroupRule(_members, memberId));

        var member = this._members.Single(x => x.IsMember(memberId));

        member.Leave();
    }

    public void SetExpirationDate(DateTime dateTo)
    {
        _paymentDateTo = dateTo;

        this.AddDomainEvent(new MeetingGroupPaymentInfoUpdatedDomainEvent(this.Id, _paymentDateTo.Value));
    }

    public Meeting CreateMeeting(
        string title,
        MeetingTerm term,
        string description,
        MeetingLocation location,
        int? attendeesLimit,
        int guestsLimit,
        Term rsvpTerm,
        MoneyValue eventFee,
        List<MemberId> hostsMembersIds,
        MemberId creatorId)
    {
        this.CheckRule(new MeetingCanBeOrganizedOnlyByPayedGroupRule(_paymentDateTo));

        this.CheckRule(new MeetingHostMustBeAMeetingGroupMemberRule(creatorId, hostsMembersIds, _members));

        return Meeting.CreateNew(
            this.Id,
            title,
            term,
            description,
            location,
            MeetingLimits.Create(attendeesLimit, guestsLimit),
            rsvpTerm,
            eventFee,
            hostsMembersIds,
            creatorId);
    }

    internal bool IsMemberOfGroup(MemberId attendeeId)
    {
        return _members.Any(x => x.IsMember(attendeeId));
    }

    internal bool IsOrganizer(MemberId memberId)
    {
        return _members.Any(x => x.IsOrganizer(memberId));
    }
}

```

### Description

A *Meeting Group* is something we want to follow in time (it has a life cycle). For this reason, it has its unique identifier (`Id`). Is should be fully encapsulated - you can only mutate its state via exposed *behavior* (no setters).