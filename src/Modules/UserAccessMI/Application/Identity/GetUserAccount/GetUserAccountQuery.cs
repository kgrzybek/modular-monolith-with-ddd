using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Identity.GetUserAccount;

public class GetUserAccountQuery : QueryBase<Result<UserAccountDto>>
{
    public GetUserAccountQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}