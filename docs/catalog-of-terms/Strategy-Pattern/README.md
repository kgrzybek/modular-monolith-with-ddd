# Strategy Pattern

## Definition

*The strategy pattern (also known as the policy pattern) is a behavioral software design pattern that enables selecting an algorithm at runtime. Instead of implementing a single algorithm directly, code receives run-time instructions as to which in a family of algorithms to use.*

Source: [Wikipedia](https://en.wikipedia.org/wiki/Strategy_pattern)

## Example

### Model

![](http://www.plantuml.com/plantuml/png/ZLBjQi904Fn_Jt7OFzgIDoX5au8LAaBr1SxBr8JaDdAteORQTw_n4jkh1HC8x6Pc9xCVKyVAScrAiLUwK3l8qHm4c-kHl7i-F_9Jw58v9vCo1_4fbP7K5J4EBBv43_eFBVeW7NZDLZ5spTXrmQJbviImxqf3zhWlm06wPemdWQ2sOFJ4sM1cVCLikPV-bW3dj4igOq1ytUOiJKFAQrwW3HMbBwUtXFKpC07hnqzOxSqa6NJnLfSORskN4vnhapypMSI_C5-tSB6j2b3GhnTE5CLelQKhAfkuwNuxjrMzNy9iOOVAKCMTKjniCgoLWHTOLXQRJD6ADFuNQ-MoF8M4sNCZpRdC2T7TIfHM7nGTd_hp4FsPt4VHvGH47yZ7OnHTB_u67WinP6KNAo5J1Aat5kn119ctC7W3DmM6prvCGMbjocy0)

### Code

```csharp
// Note: code has been abbreviated for simplicitiy's sake.

internal class AddMeetingCommentLikeCommandHandler : ICommandHandler<AddMeetingCommentLikeCommand>
{
	...
	public async Task<Unit> Handle(AddMeetingCommentLikeCommand request, CancellationToken cancellationToken)
    {
        var meetingComment = await _meetingCommentRepository.GetByIdAsync(new MeetingCommentId(request.MeetingCommentId));
        var like = meetingComment.Like(_memberContext.MemberId, likerMeetingGroupMemberData, meetingMemeberCommentLikesCount);
    }
    ...
}

public abstract class Entity
{
	...
	protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
	...
}

public class MeetingComment : Entity, IAggregateRoot
{
	...
	 public MeetingMemberCommentLike Like(MemberId likerId,
            MeetingGroupMemberData likerMeetingGroupMember,
            int meetingMemberCommentLikesCount)
     {
         this.CheckRule(new CommentCanBeLikedOnlyByMeetingGroupMemberRule(likerMeetingGroupMember));
         this.CheckRule(new CommentCannotBeLikedByTheSameMemberMoreThanOnceRule(meetingMemberCommentLikesCount));
     }
     ...
}

public class CommentCanBeLikedOnlyByMeetingGroupMemberRule : IBusinessRule
{
	...
    public bool IsBroken() => _likerMeetingGroupMember == null;
    ...
}

public class CommentCannotBeLikedByTheSameMemberMoreThanOnceRule : IBusinessRule
{
	...
    public bool IsBroken() => _memberCommentLikesCount > 0;
	...
}

public interface IBusinessRule
{
	...
    bool IsBroken();
    ...
}
```
### Description

Maybe not obvious at first glance, but any class that extends `Entity` and checks for violation of `BusinessRules` is an example of the strategy pattern.

---

Let's introduce the concepts of the strategy pattern, so we can understand how the above example fits this pattern.

* **Client** - The calling code.
* **Context** - An object which maintains a reference to one of the *concrete strategies* and communicates with the *client*.
* **Strategy interface** - An interface or abstract class that the *client* can use to set a concrete strategy at run-time, through the *context*.
* **Concrete strategies** - One or more implementations of the *strategy interface*.

---

If we have a close look at our example of adding a 'like' to a `MeetingComment`, we can notice the elements of the strategy pattern.

* `AddMeetingCommentLikeCommandHandler` is the calling code, so it represents the **Client**. 
* `MeetingComment` is the object which maintains a reference to a sets of business rules, so it represents the **Context**. Since `MeetingComment` extends `Entity` it has access to `CheckRule`. This method is used to *set the strategy*, which in this case is any kind of business rule.
* `IBusinessRule` represents the **Strategy interface**.
* `CommentCanBeLikedOnlyByMeetingGroupMemberRule` & `CommentCannotBeLikedByTheSameMemberMoreThanOnceRule` are the implementations of `IBusinessRule` so they represent the **Concrete strategies**.

---

Strategy should not be confused with [Decorator](../Decorator-Pattern/)!!!
*A strategy lets you change the guts of an object, while decorator lets you change the skin.*