using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.RequestForgotPasswordLink;

public class RequestForgotPasswordLinkCommand : CommandBase<ForgotPasswordLinkResult>
{
    public RequestForgotPasswordLinkCommand(string emailAddress)
    {
        EmailAddress = emailAddress;
    }

    public string EmailAddress { get; }
}