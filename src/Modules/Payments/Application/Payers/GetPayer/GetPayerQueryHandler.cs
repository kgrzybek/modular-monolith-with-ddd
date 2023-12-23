using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
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

            const string sql = $"""
                               SELECT 
                                   [Payer].[Id] as [{nameof(PayerDto.Id)}], 
                                   [Payer].[Login] as [{nameof(PayerDto.Login)}], 
                                   [Payer].[Email] as [{nameof(PayerDto.Email)}], 
                                   [Payer].[FirstName] as [{nameof(PayerDto.FirstName)}], 
                                   [Payer].[LastName] as [{nameof(PayerDto.LastName)}], 
                                   [Payer].[Name] as [{nameof(PayerDto.Name)}]
                               FROM [payments].[Payers] AS [Payer] 
                               WHERE [Payer].[Id] = @PayerId
                               """;

            return await connection.QuerySingleAsync<PayerDto>(sql, new
            {
                query.PayerId
            });
        }
    }
}