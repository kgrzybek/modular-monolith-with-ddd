using System;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    internal class SendUserRegistrationConfirmationEmailCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal SendUserRegistrationConfirmationEmailCommand(Guid id, UserRegistrationId userRegistrationId, string email)
        : base(id)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
        }

        internal UserRegistrationId UserRegistrationId { get; }

        internal string Email { get; }
    }
}