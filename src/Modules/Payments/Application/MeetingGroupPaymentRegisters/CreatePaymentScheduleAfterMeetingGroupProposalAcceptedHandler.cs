using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.CreatePaymentRegister;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters
{
    public class CreatePaymentScheduleAfterMeetingGroupProposalAcceptedHandler : INotificationHandler<MeetingGroupProposalAcceptedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public CreatePaymentScheduleAfterMeetingGroupProposalAcceptedHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MeetingGroupProposalAcceptedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(
                new CreatePaymentRegisterCommand(
                    Guid.NewGuid(),
                    notification.MeetingGroupProposalId));
        }
    }
}