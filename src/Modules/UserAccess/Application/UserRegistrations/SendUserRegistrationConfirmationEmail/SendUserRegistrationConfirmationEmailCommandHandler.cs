using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    internal class SendUserRegistrationConfirmationEmailCommandHandler : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
    {
        private IEmailSender _emailSender;

        public SendUserRegistrationConfirmationEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task<Unit> Handle(SendUserRegistrationConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage(request.Email, "MyMeetings - Please confirm your registration",
                "This should be link to confirmation page. For now, please execute HTTP request " +
                $"PATCH http://localhost:5000/userAccess/userRegistrations/{request.UserRegistrationId.Value}/confirm");

            _emailSender.SendEmail(emailMessage);

            return Unit.Task;
        }
    }
}