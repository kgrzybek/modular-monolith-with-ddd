# Dependency Injection

## Definition

*Dependency Injection is a technique in which an object receives other objects that it depends on. These other objects are called dependencies.*

Source: [Wikipedia](https://en.wikipedia.org/wiki/Dependency_injection)

## Example

### Model

![](http://www.plantuml.com/plantuml/png/HSun3i8m30NGdLF00LBlJ1rOE4PgcpOqiIl7KLLEJpeWbl-bfp_yiNeqRoLVRaamD-9c-RguR_KEO74VvkHBcrfbGnLdyG6rm3hRvvXuXQBKShHGL3JtQTZF828eiJeRa685Z1wppa5VeLkfyE2DXLZm24zvCtfI0VfZ-k6mdUV6xhs_)

### Code

```csharp

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

A `CancelMeetingCommandHandler` needs two collaborators (dependencies) to fulfill its job - repository of meetings (`IMeetingRepository`) and information about member context (`IMemberContext`). It doesn't instance implementation of these interfaces itself - they are provided (injected) via construction (*Constructor Injection*).