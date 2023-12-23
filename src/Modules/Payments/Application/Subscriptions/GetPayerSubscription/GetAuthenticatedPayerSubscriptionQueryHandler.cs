using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetPayerSubscription
{
    internal class GetAuthenticatedPayerSubscriptionQueryHandler : IQueryHandler<GetAuthenticatedPayerSubscriptionQuery, SubscriptionDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetAuthenticatedPayerSubscriptionQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<SubscriptionDetailsDto> Handle(GetAuthenticatedPayerSubscriptionQuery query, CancellationToken cancellationToken)
        {
            const string sql = $"""
                                SELECT
                                   [SubscriptionDetails].[Id] AS [{nameof(SubscriptionDetailsDto.SubscriptionId)}],
                                   [SubscriptionDetails].[Period] AS [{nameof(SubscriptionDetailsDto.Period)}],
                                   [SubscriptionDetails].[Status] AS [{nameof(SubscriptionDetailsDto.Status)}],
                                   [SubscriptionDetails].[ExpirationDate] AS [{nameof(SubscriptionDetailsDto.ExpirationDate)}]
                                FROM [payments].[SubscriptionDetails] AS [SubscriptionDetails]
                                WHERE [SubscriptionDetails].[PayerId] = @PayerId
                                """;

            var connection = _sqlConnectionFactory.GetOpenConnection();

            return await connection.QuerySingleOrDefaultAsync<SubscriptionDetailsDto>(
                sql,
                new
                {
                    PayerId = _executionContextAccessor.UserId
                });
        }
    }
}