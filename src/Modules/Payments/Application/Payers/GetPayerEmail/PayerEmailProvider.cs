using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayerEmail
{
    public static class PayerEmailProvider
    {
        public static async Task<string> GetPayerEmail(Guid payerId, ISqlConnectionFactory sqlConnectionFactory)
        {
            var connection = sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT 
                                    [Payer].[Email] 
                                FROM [payments].[Payers] AS [Payer] 
                                WHERE [Payer].[Id] = @PayerId
                                """;

            return await connection.QuerySingleAsync<string>(sql, new
            {
                payerId
            });
        }
    }
}