using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayerEmail
{
    public static class PayerEmailProvider
    {
        private static ISqlConnectionFactory _sqlConnectionFactory;
        
        public static void Init(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public static async Task<PayerDto> GetPayerDetails(Guid payerId)
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