using CompanyName.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    public class SendUserRegistrationConfirmationEmailCommand : InternalCommandBase
    {
        [JsonConstructor]
        public SendUserRegistrationConfirmationEmailCommand(
            Guid id,
            UserRegistrationId userRegistrationId,
            string email,
            string confirmLink)
            : base(id)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
            ConfirmLink = confirmLink;
        }

        internal UserRegistrationId UserRegistrationId { get; }

        internal string Email { get; }

        internal string ConfirmLink { get; }
    }
}