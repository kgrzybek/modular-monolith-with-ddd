using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.GetMeetingGroupPayment;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.GetMeetingGroupPayments
{
    public class GetMeetingGroupPaymentsQueryHandler : IQueryHandler<GetMeetingGroupPaymentsQuery, List<MeetingGroupPaymentDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingGroupPaymentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingGroupPaymentDto>> Handle(GetMeetingGroupPaymentsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql = "SELECT " +
                      $"[MeetingGroupPayment].[Id] AS [{nameof(MeetingGroupPaymentDto.Id)}], " +
                      $"[MeetingGroupPayment].[MeetingGroupPaymentRegisterId] AS [{nameof(MeetingGroupPaymentDto.MeetingGroupPaymentRegisterId)}], " +
                      $"[MeetingGroupPayment].[PayerId] AS [{nameof(MeetingGroupPaymentDto.PayerId)}], " +
                      $"[MeetingGroupPayment].[PaymentTermEndDate] AS [{nameof(MeetingGroupPaymentDto.PaymentTermEndDate)}], " +
                      $"[MeetingGroupPayment].[PaymentTermStartDate] AS [{nameof(MeetingGroupPaymentDto.PaymentTermStartDate)}], " +
                      $"[MeetingGroupPayment].[Date] AS [{nameof(MeetingGroupPaymentDto.Date)}] " +
                      "FROM [payments].[v_MeetingGroupPayments] AS [MeetingGroupPayment] " +
                      "WHERE [MeetingGroupPayment].[MeetingGroupPaymentRegisterId] = @MeetingGroupPaymentRegisterId";

            var payments = await connection.QueryAsync<MeetingGroupPaymentDto>(sql,
                new {query.MeetingGroupPaymentRegisterId});

            return payments.AsList();
        }
    }
}