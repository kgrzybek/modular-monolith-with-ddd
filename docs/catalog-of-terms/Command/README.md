# Command

## Definition

*A command is a request made to do something. A command represents the intention of a system’s user regarding what the system will do to change its state.*

*Command characteristics:*

- *The result of a command can be either success or failure; the result is an [Event(s)](../Event/)*

- *In case of success, state change(s) must have occurred somewhere (otherwise nothing happened)*

- *Commands should be named with a verb, in the present tense or infinitive, and a nominal group coming from the domain (entity of aggregate type)*

Source: [Open Agile Architecture](https://pubs.opengroup.org/architecture/o-aa-standard/#KLP-EDA-event-command)


## Example

### Model

### Code

`Meeting` class in [Domain Model](../Domain-Model/):

```csharp

public void Cancel(MemberId cancelMemberId)
{
    this.CheckRule(new MeetingCannotBeChangedAfterStartRule(_term));

    if (!_isCanceled)
    {
        _isCanceled = true;
        _cancelDate = SystemClock.Now;
        _cancelMemberId = cancelMemberId;

        this.AddDomainEvent(new MeetingCanceledDomainEvent(this.Id, _cancelMemberId, _cancelDate.Value));
    }
}

```

`CancelMeetingCommand` class in [Application Layer](../Application-Layer/)

```csharp

public class CancelMeetingCommand : CommandBase
{
    public CancelMeetingCommand(Guid meetingId)
    {
        MeetingId = meetingId;
    }

    public Guid MeetingId { get; }
}

internal class CancelMeetingCommandHandler : ICommandHandler<CancelMeetingCommand>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMemberContext _memberContext;

    internal CancelMeetingCommandHandler(IMeetingRepository meetingRepository, IMemberContext memberContext)
    {
        _meetingRepository = meetingRepository;
        _memberContext = memberContext;
    }

    public async Task<Unit> Handle(CancelMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        meeting.Cancel(_memberContext.MemberId);

        return Unit.Value;
    }
}

```

### Description

In the example above, we can distinguish between 2 types of Commands:
- as an object in the application layer [Parameter Object Pattern](../Parameter-Object-Pattern/)
- in the form of a method on the object (in DDD on the [Aggregate](../Aggregate-DDD/)

The most important thing is that the Command can be rejected until the state changes. In both the `CommandHandler` (invalid MeetingId) and the `Cancel` method (business rule broken), an exception can be thrown and the Command is then rejected and all uncomitted changes are rolled back (state does not change). 