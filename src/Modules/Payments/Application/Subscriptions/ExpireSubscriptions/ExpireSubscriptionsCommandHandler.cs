using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptions
{
    internal class ExpireSubscriptionsCommandHandler : ICommandHandler<ExpireSubscriptionsCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly ICommandsScheduler _commandsScheduler;

        public ExpireSubscriptionsCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory, 
            ICommandsScheduler commandsScheduler)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _commandsScheduler = commandsScheduler;
        }

        public async Task<Unit> Handle(ExpireSubscriptionsCommand request, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                               "[SubscriptionDetails].Id " +
                               "FROM [payments].[SubscriptionDetails] AS [SubscriptionDetails] " +
                               "WHERE [SubscriptionDetails].ExpirationDate < @Date";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            var expiredSubscriptionsIds = 
                await connection.QueryAsync<Guid>(sql, new
            {
                Date = SystemClock.Now
            });

            var expiredSubscriptionsIdsList = expiredSubscriptionsIds.AsList();

            foreach (var subscriptionId in expiredSubscriptionsIdsList)
            {
                await _commandsScheduler.EnqueueAsync(
                    new ExpireSubscriptionCommand(
                        Guid.NewGuid(),
                        subscriptionId));
            }

            return Unit.Value;
        }
    }
}