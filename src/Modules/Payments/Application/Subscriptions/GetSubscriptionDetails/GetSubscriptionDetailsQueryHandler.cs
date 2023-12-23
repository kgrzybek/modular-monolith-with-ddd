using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails
{
    internal class GetSubscriptionDetailsQueryHandler : IQueryHandler<GetSubscriptionDetailsQuery, SubscriptionDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetSubscriptionDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<SubscriptionDetailsDto> Handle(GetSubscriptionDetailsQuery query, CancellationToken cancellationToken)
        {
            const string sql = $"""
                               SELECT 
                                   [SubscriptionDetails].[Id] AS [{nameof(SubscriptionDetailsDto.SubscriptionId)}],
                                   [SubscriptionDetails].[Period] AS [{nameof(SubscriptionDetailsDto.Period)}],
                                   [SubscriptionDetails].[Status] AS [{nameof(SubscriptionDetailsDto.Status)}], 
                                   [SubscriptionDetails].[ExpirationDate] AS [{nameof(SubscriptionDetailsDto.ExpirationDate)}] 
                               FROM [payments].[SubscriptionDetails] AS [SubscriptionDetails] 
                               WHERE [SubscriptionDetails].[Id] = @SubscriptionId
                               """;

            var connection = _sqlConnectionFactory.GetOpenConnection();

            return await connection.QuerySingleAsync<SubscriptionDetailsDto>(
                sql,
                new
                {
                    query.SubscriptionId
                });
        }
    }
}