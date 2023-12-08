using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.SendSubscriptionRenewalConfirmationEmail
{
    internal class SendSubscriptionRenewalConfirmationEmailCommandHandler : ICommandHandler<SendSubscriptionRenewalConfirmationEmailCommand>
    {
        private readonly IEmailSender _emailSender;

        public SendSubscriptionRenewalConfirmationEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(SendSubscriptionRenewalConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage(
                request.Email,
                "MyMeetings - Subscription renewed",
                $"Subscription {request.SubscriptionId.Value} was successfully paid and renewed with ❤ for you!");

            await _emailSender.SendEmail(emailMessage);
        }
    }
}