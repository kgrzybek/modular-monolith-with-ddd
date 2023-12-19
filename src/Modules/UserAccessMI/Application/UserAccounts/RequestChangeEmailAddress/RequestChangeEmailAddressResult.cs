using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.RequestChangeEmailAddress;

public class RequestChangeEmailAddressResult : Result
{
    public RequestChangeEmailAddressResult()
    {
    }

    public RequestChangeEmailAddressResult(string token)
        : base()
    {
        Token = token;
    }

    public string? Token { get; private set; }
}