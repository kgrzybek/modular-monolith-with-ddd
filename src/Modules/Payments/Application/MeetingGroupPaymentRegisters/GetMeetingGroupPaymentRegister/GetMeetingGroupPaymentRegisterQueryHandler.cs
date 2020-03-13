using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.GetMeetingGroupPaymentRegister
{
    internal class GetMeetingGroupPaymentRegisterQueryHandler : IQueryHandler<GetMeetingGroupPaymentRegisterQuery, MeetingGroupPaymentRegisterDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingGroupPaymentRegisterQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingGroupPaymentRegisterDto> Handle(GetMeetingGroupPaymentRegisterQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT " +
                         $"[MeetingGroupPaymentRegister].[Id] AS [{nameof(MeetingGroupPaymentRegisterDto.Id)}], " +
                         $"[MeetingGroupPaymentRegister].[CreateDate] AS [{nameof(MeetingGroupPaymentRegisterDto.CreateDate)}] " +
                         "FROM [payments].[v_MeetingGroupPaymentRegisters] AS [MeetingGroupPaymentRegister] " +
                         "WHERE [MeetingGroupPaymentRegister].[Id] = @MeetingGroupPaymentRegisterId";

            return await connection.QuerySingleAsync<MeetingGroupPaymentRegisterDto>(sql,
                new {query.MeetingGroupPaymentRegisterId});
        }
    }
}