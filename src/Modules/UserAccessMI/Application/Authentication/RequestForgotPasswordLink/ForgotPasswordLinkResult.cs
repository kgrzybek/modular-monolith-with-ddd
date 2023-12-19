using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.RequestForgotPasswordLink;

public class ForgotPasswordLinkResult : Result
{
    public ForgotPasswordLinkResult()
    {
    }

    public ForgotPasswordLinkResult(string token)
    {
        Token = token;
    }

    public string? Token { get; set; }
}