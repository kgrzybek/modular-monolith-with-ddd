using System.Data;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments
{
    internal class SubscriptionPaymentsProjector : ProjectorBase, IProjector
    {
        private readonly IDbConnection _connection;

        public SubscriptionPaymentsProjector(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connection = sqlConnectionFactory.GetOpenConnection();
        }

        public async Task Project(IDomainEvent @event)
        {
            await When((dynamic)@event);
        }

        private async Task When(SubscriptionPaymentCreatedDomainEvent subscriptionPaymentCreated)
        {
            string period = SubscriptionPeriod.GetName(subscriptionPaymentCreated.SubscriptionPeriodCode);

            await _connection.ExecuteScalarAsync(
                "INSERT INTO payments.SubscriptionPayments " +
                    "([PaymentId], [PayerId], [Type], [Status], [Period], [Date], " +
                "[SubscriptionId], [MoneyValue], [MoneyCurrency]) " +
                "VALUES (@SubscriptionPaymentId, @PayerId, @Type, @Status, @Period, " +
                "@OccurredOn, NULL, @Value, @Currency)",
                new
                {
                    subscriptionPaymentCreated.SubscriptionPaymentId,
                    subscriptionPaymentCreated.PayerId,
                    Type = "Initial Payment",
                    subscriptionPaymentCreated.Status,
                    period,
                    subscriptionPaymentCreated.OccurredOn,
                    subscriptionPaymentCreated.Value,
                    subscriptionPaymentCreated.Currency
                });
        }

        private async Task When(SubscriptionPaymentPaidDomainEvent subscriptionPaymentPaid)
        {
            await _connection.ExecuteScalarAsync(
                "UPDATE payments.SubscriptionPayments SET Status = @Status " +
                "WHERE PaymentId = @SubscriptionPaymentId ",
                new
                {
                    subscriptionPaymentPaid.SubscriptionPaymentId,
                    subscriptionPaymentPaid.Status
                });
        }

        private async Task When(SubscriptionCreatedDomainEvent subscriptionCreated)
        {
            await _connection.ExecuteScalarAsync(
                "UPDATE payments.SubscriptionPayments SET SubscriptionId = @SubscriptionId " +
                "WHERE PaymentId = @SubscriptionPaymentId ",
                new
                {
                    subscriptionCreated.SubscriptionId,
                    subscriptionCreated.SubscriptionPaymentId
                });
        }
    }
}