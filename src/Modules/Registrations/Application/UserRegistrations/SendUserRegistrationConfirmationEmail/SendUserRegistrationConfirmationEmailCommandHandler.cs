using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.Registrations.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    internal class SendUserRegistrationConfirmationEmailCommandHandler : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
    {
        private readonly IEmailSender _emailSender;

        public SendUserRegistrationConfirmationEmailCommandHandler(
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(SendUserRegistrationConfirmationEmailCommand command, CancellationToken cancellationToken)
        {
            string link = $"<a href=\"{command.ConfirmLink}{command.UserRegistrationId.Value}\">link</a>";

            string content = $"Welcome to MyMeetings application! Please confirm your registration using this {link}.";

            var emailMessage = new EmailMessage(
                command.Email,
                "MyMeetings - Please confirm your registration",
                content);

            await _emailSender.SendEmail(emailMessage);
        }
    }
}