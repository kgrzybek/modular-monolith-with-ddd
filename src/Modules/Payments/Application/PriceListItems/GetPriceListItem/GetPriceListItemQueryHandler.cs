using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.GetPriceListItem
{
    internal class GetPriceListItemQueryHandler : IQueryHandler<GetPriceListItemQuery, PriceListItemMoneyValueDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPriceListItemQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PriceListItemMoneyValueDto> Handle(
            GetPriceListItemQuery query,
            CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT 
                                    [PriceListItem].[MoneyCurrency] AS [{nameof(PriceListItemMoneyValueDto.Currency)}], 
                                    [PriceListItem].[MoneyValue] AS [{nameof(PriceListItemMoneyValueDto.Value)}] 
                                FROM [payments].[PriceListItems] AS [PriceListItem] 
                                WHERE 
                                    [PriceListItem].[IsActive] = 1 
                                    AND [PriceListItem].[SubscriptionPeriodCode] = @PeriodTypeCode 
                                    AND [PriceListItem].[CategoryCode] = @CategoryCode 
                                    AND [PriceListItem].[CountryCode] = @CountryCode 
                                """;
            return await connection.QuerySingleAsync<PriceListItemMoneyValueDto>(
                sql,
                new
                {
                    query.CategoryCode,
                    query.PeriodTypeCode,
                    query.CountryCode
                });
        }
    }
}