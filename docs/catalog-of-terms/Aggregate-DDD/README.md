# Aggregate (DDD)

## Definition

*Cluster ENTITES and VALUE OBJECTS into AGGREGATES and define boundaries around each. Choose one ENTITY to be the root of each AGGREGATE, and control all access to the objects inside the boundary through the root. Transient references to internal members can be passed out for use within a single operation only. Because the root controls access, it cannot be blindsided by changes to the internals. This arrangement makes it practical to enforce all invariants for objects in the AGGREGATE and for the AGGREGATE as a whole in any state change.*

Source: [Domain-Driven Design: Tackling Complexity in the Heart of Software, Eric Evans](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215)

## Example

### Model

![](http://www.plantuml.com/plantuml/png/FOx13i8m34FlV0LyG9Sxfo7rHx8cTDFQPaeJJV3rT1SkjcpPqfkxePhNSdjiBHKdTYttrUpeJm35SygRhRvuPqtIZ9jDIIhiMR-VXNUeGbvGGvKcPKp3UGaHGSLkh42IEYGqB9A3lCFeQeTNpiePZKEC4V2Vnd4wBfoP6mt_0G00)

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

Classes `MeetingGroup`, `MeetingGroupLocation`, `MeetingGroupId`, `MeetingGroupMember', 'MeetingGroupMemberRole` form the **Aggregate**. `MeetingGroup` acts as the **AggregateRoot** (*Choose one ENTITY to be the root of each AGGREGATE*). AggregateRoot has a global identifier (`Id`) and public methods to change state of the *Aggregate*. The rest is encapsulated (*Because the root controls access, it cannot be blindsided by changes to the internals*). For each public method, the invariants are checked first (`CheckRule`) (*This arrangement makes it practical to enforce all invariants for objects in the AGGREGATE and for the AGGREGATE as a whole in any state change*).

## Additional References

- [DDD_Aggregate (Martin Fowler)](https://martinfowler.com/bliki/DDD_Aggregate.html)