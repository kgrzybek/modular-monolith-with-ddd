using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.SendSubscriptionCreationConfirmationEmail
{
    internal class SendSubscriptionCreationConfirmationEmailCommandHandler : ICommandHandler<SendSubscriptionCreationConfirmationEmailCommand>
    {
        private readonly IEmailSender _emailSender;

        public SendSubscriptionCreationConfirmationEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(SendSubscriptionCreationConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage(
                request.Email,
                "MyMeetings - Subscription purchased",
                $"Subscription {request.SubscriptionId.Value} was successfully paid and created with ❤ for you!");

            await _emailSender.SendEmail(emailMessage);
        }
    }
}