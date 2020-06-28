using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptionPayment;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptions
{
    public class ExpireSubscriptionPaymentsCommandHandler : ICommandHandler<ExpireSubscriptionPaymentsCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly ICommandsScheduler _commandsScheduler;

        public ExpireSubscriptionPaymentsCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory, 
            ICommandsScheduler commandsScheduler)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _commandsScheduler = commandsScheduler;
        }

        public async Task<Unit> Handle(ExpireSubscriptionPaymentsCommand request, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                               "[SubscriptionPayment].PaymentId " +
                               "FROM [payments].[SubscriptionPayments] AS [SubscriptionPayment] " +
                               "WHERE [SubscriptionPayment].Date > @Date";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            var timeForPayment = TimeSpan.FromMinutes(20);
            var date = SystemClock.Now.Add(-timeForPayment);

            var expiredSubscriptionPaymentsIds = 
                await connection.QueryAsync<Guid>(sql, new
            {
                Date = date
            });

            var expiredSubscriptionsPaymentsIdsList = expiredSubscriptionPaymentsIds.AsList();

            foreach (var subscriptionPaymentId in expiredSubscriptionsPaymentsIdsList)
            {
                await _commandsScheduler.EnqueueAsync(
                    new ExpireSubscriptionPaymentCommand(subscriptionPaymentId));
            }

            return Unit.Value;
        }
    }
}