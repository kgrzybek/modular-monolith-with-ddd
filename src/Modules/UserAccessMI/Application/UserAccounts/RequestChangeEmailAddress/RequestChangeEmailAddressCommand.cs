using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.RequestChangeEmailAddress
{
    public class RequestChangeEmailAddressCommand : CommandBase<RequestChangeEmailAddressResult>
    {
        public RequestChangeEmailAddressCommand(Guid userId, string newEmailAddress)
        {
            UserId = userId;
            NewEmailAddress = newEmailAddress;
        }

        public Guid UserId { get; }

        public string NewEmailAddress { get; }
    }
}