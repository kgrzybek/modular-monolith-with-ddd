using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer;
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
            var payer = await GetPayerDetails(notification.DomainEvent.PayerId);
            
            await _commandsScheduler.EnqueueAsync(new SendSubscriptionRenewalConfirmationEmailCommand(
                Guid.NewGuid(),
                new SubscriptionId(notification.DomainEvent.SubscriptionId),
                payer.Email));
        }
        
        private async Task<PayerDto> GetPayerDetails(Guid payerId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Payer].[Id], " +
                               "[Payer].[Login], " +
                               "[Payer].[Email], " +
                               "[Payer].[FirstName], " +
                               "[Payer].[LastName], " +
                               "[Payer].[Name] " +
                               "FROM [payments].[Payers] AS [Payer] " +
                               "WHERE [Payer].[Id] = @PayerId";

            return await connection.QuerySingleAsync<PayerDto>(sql, new
            {
                payerId
            });
        }
    }
}