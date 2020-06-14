using System.Data;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails
{
    internal class SubscriptionDetailsProjector : ProjectorBase, IProjector
    {
        private readonly IDbConnection _connection;

        public SubscriptionDetailsProjector(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connection = sqlConnectionFactory.GetOpenConnection();
        }

        public async Task Project(IDomainEvent @event)
        {
            await When((dynamic) @event);
        }

        private async Task When(SubscriptionRenewedDomainEvent subscriptionRenewed)
        {
            var period = GetPeriodName(subscriptionRenewed.SubscriptionPeriodCode);
            
            await _connection.ExecuteScalarAsync("UPDATE payments.SubscriptionDetails " +
                                                    "SET " +
                                                        "[Status] = @Status, " +
                                                        "[ExpirationDate] = @ExpirationDate, " +
                                                        "[Period] = @Period " +
                                                    "WHERE [Id] = @SubscriptionId",
                new
                {
                    subscriptionRenewed.SubscriptionId,
                    subscriptionRenewed.Status,
                    subscriptionRenewed.ExpirationDate,
                    period
                });
        }

        private async Task When(SubscriptionExpiredDomainEvent subscriptionExpired)
        {
            await _connection.ExecuteScalarAsync("UPDATE payments.SubscriptionDetails " +
                                                 "SET " +
                                                 "[Status] = @Status " +
                                                 "WHERE [Id] = @SubscriptionId",
                new
                {
                    subscriptionExpired.SubscriptionId,
                    subscriptionExpired.Status
                });
        }

        private async Task When(SubscriptionPurchasedDomainEvent subscriptionPurchased)
        {
            var period = GetPeriodName(subscriptionPurchased.SubscriptionPeriodCode);
            
            await _connection.ExecuteScalarAsync("INSERT INTO payments.SubscriptionDetails " +
                                           "([Id], [Period], [Status], [CountryCode], [ExpirationDate]) " +
                                           "VALUES (@SubscriptionId, @Period, @Status, @CountryCode, @ExpirationDate)",
                new
                {
                    subscriptionPurchased.SubscriptionId,
                    period,
                    subscriptionPurchased.Status,
                    subscriptionPurchased.CountryCode,
                    subscriptionPurchased.ExpirationDate
                });
        }

        private static string GetPeriodName(string subscriptionPeriodCode)
        {
            return subscriptionPeriodCode == SubscriptionPeriod.Month.Code
                ? "Month"
                : "6 months";
        }
    }
}