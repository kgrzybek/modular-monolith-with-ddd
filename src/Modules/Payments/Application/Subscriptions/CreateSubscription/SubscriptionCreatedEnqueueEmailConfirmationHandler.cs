using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayerEmail;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.SendSubscriptionCreationConfirmationEmail;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription
{
    public class SubscriptionCreatedEnqueueEmailConfirmationHandler : INotificationHandler<SubscriptionCreatedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SubscriptionCreatedEnqueueEmailConfirmationHandler(
            ICommandsScheduler commandsScheduler,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _commandsScheduler = commandsScheduler;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task Handle(SubscriptionCreatedNotification notification, CancellationToken cancellationToken)
        {
            PayerEmailProvider.Init(_sqlConnectionFactory);
            
            var payer = await PayerEmailProvider.GetPayerDetails(notification.DomainEvent.PayerId);
            
            await _commandsScheduler.EnqueueAsync(new SendSubscriptionCreationConfirmationEmailCommand(
                Guid.NewGuid(),
                new SubscriptionId(notification.DomainEvent.SubscriptionId),
                payer.Email));
        }
    }
}