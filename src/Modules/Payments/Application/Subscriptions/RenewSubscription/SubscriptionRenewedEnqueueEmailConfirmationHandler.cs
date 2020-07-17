using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayerEmail;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.SendSubscriptionRenewalConfirmationEmail;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription
{
    public class SubscriptionRenewedEnqueueEmailConfirmationHandler : INotificationHandler<SubscriptionRenewedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SubscriptionRenewedEnqueueEmailConfirmationHandler(
            ICommandsScheduler commandsScheduler,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _commandsScheduler = commandsScheduler;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public async Task Handle(SubscriptionRenewedNotification notification, CancellationToken cancellationToken)
        {
            PayerEmailProvider.Init(_sqlConnectionFactory);
            
            var payer = await PayerEmailProvider.GetPayerDetails(notification.DomainEvent.PayerId);
            
            await _commandsScheduler.EnqueueAsync(new SendSubscriptionRenewalConfirmationEmailCommand(
                Guid.NewGuid(),
                new SubscriptionId(notification.DomainEvent.SubscriptionId),
                payer.Email));
        }
    }
}