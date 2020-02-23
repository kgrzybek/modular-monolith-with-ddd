using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer
{
    internal class GetPayerQueryHandler : IQueryHandler<GetPayerQuery, PayerDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPayerQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PayerDto> Handle(GetPayerQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Payer].[Id], " +
                               "[Payer].[Login], " +
                               "[Payer].[Email], " +
                               "[Payer].[FirstName], " +
                               "[Payer].[LastName], " +
                               "[Payer].[Name] " +
                               "FROM [payments].[v_Payers] AS [Payer] " +
                               "WHERE [Payer].[Id] = @PayerId";

            return await connection.QuerySingleAsync<PayerDto>(sql, new
            {
                query.PayerId
            });
        }
    }
}