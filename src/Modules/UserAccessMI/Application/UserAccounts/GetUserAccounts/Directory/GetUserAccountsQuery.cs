using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.GetUserAccounts.Directory;

public class GetUserAccountsQuery : QueryBase<Result<IEnumerable<UserAccountDto>>>
{
}