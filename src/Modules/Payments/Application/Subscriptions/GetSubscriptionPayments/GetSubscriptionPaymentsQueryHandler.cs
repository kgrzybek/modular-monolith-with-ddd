using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments
{
    internal class
        GetSubscriptionPaymentsQueryHandler : IQueryHandler<GetSubscriptionPaymentsQuery, List<SubscriptionPaymentDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetSubscriptionPaymentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<SubscriptionPaymentDto>> Handle(
            GetSubscriptionPaymentsQuery query,
            CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT 
                                    [SubscriptionPayment].[PaymentId] AS [{nameof(SubscriptionPaymentDto.PaymentId)}], 
                                    [SubscriptionPayment].[PayerId] AS [{nameof(SubscriptionPaymentDto.PayerId)}], 
                                    [SubscriptionPayment].[Status] AS [{nameof(SubscriptionPaymentDto.Status)}], 
                                    [SubscriptionPayment].[MoneyCurrency] AS [{nameof(SubscriptionPaymentDto.MoneyCurrency)}], 
                                    [SubscriptionPayment].[MoneyValue] AS [{nameof(SubscriptionPaymentDto.MoneyValue)}], 
                                    [SubscriptionPayment].[Date] AS [{nameof(SubscriptionPaymentDto.Date)}], 
                                    [SubscriptionPayment].[SubscriptionId] AS [{nameof(SubscriptionPaymentDto.SubscriptionId)}], 
                                    [SubscriptionPayment].[Type] AS [{nameof(SubscriptionPaymentDto.Type)}], 
                                    [SubscriptionPayment].[Period] AS [{nameof(SubscriptionPaymentDto.Period)}] 
                                FROM [payments].[SubscriptionPayments] AS [SubscriptionPayment] 
                                WHERE [SubscriptionPayment].PayerId = @PayerId
                                """;

            var subscriptionPayments = await connection.QueryAsync<SubscriptionPaymentDto>(
                sql,
                new
                {
                    query.PayerId
                });

            return subscriptionPayments.AsList();
        }
    }
}