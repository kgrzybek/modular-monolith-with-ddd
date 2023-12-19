using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.GetUserAccounts.ById;

public class GetUserAccountsQuery : QueryBase<Result<UserAccountDto>>
{
    public GetUserAccountsQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}