using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.SendSubscriptionCreationConfirmationEmail
{
    internal class SendSubscriptionCreationConfirmationEmailCommandHandler : ICommandHandler<SendSubscriptionCreationConfirmationEmailCommand>
    {
        private readonly IEmailSender _emailSender;

        public SendSubscriptionCreationConfirmationEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        
        public Task<Unit> Handle(SendSubscriptionCreationConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage(request.Email, "MyMeetings - Subscription purchased",
                $"Subscription {request.SubscriptionId.Value} was successfully paid and created with ❤ for you!");

            _emailSender.SendEmail(emailMessage);
            return Unit.Task;
        }
    }
}