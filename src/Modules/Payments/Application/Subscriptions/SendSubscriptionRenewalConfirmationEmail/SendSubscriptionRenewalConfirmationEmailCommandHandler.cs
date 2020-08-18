using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.SendSubscriptionRenewalConfirmationEmail
{
    internal class SendSubscriptionRenewalConfirmationEmailCommandHandler : ICommandHandler<SendSubscriptionRenewalConfirmationEmailCommand>
    {
        private readonly IEmailSender _emailSender;

        public SendSubscriptionRenewalConfirmationEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task<Unit> Handle(SendSubscriptionRenewalConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage(request.Email, "MyMeetings - Subscription renewed",
                $"Subscription {request.SubscriptionId.Value} was successfully paid and renewed with ❤ for you!");

            _emailSender.SendEmail(emailMessage);
            return Unit.Task;
        }
    }
}