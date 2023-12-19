using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.AuthenticatorRegistration.GetAuthenticatorKey;

public class GetAuthenticatorKeyQuery : QueryBase<Result<string>>
{
    public GetAuthenticatorKeyQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}