using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments.GetMeetingPayment
{
    public class GetMeetingPaymentQueryHandler : IQueryHandler<GetMeetingPaymentQuery, MeetingPaymentDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingPaymentQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingPaymentDto> Handle(GetMeetingPaymentQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            return await connection.QuerySingleAsync<MeetingPaymentDto>("SELECT " +
                                              $"[MeetingPayment].[PayerId] AS [{nameof(MeetingPaymentDto.PayerId)}], " +
                                              $"[MeetingPayment].[FeeCurrency] AS [{nameof(MeetingPaymentDto.FeeCurrency)}], " +
                                              $"[MeetingPayment].[FeeValue] AS [{nameof(MeetingPaymentDto.FeeValue)}], " +
                                              $"[MeetingPayment].[MeetingId] AS [{nameof(MeetingPaymentDto.MeetingId)}], " +
                                              $"[MeetingPayment].[PaymentDate] AS [{nameof(MeetingPaymentDto.PaymentDate)}], " +
                                              $"[MeetingPayment].[CreateDate] AS [{nameof(MeetingPaymentDto.CreateDate)}] " +
                                              "FROM [payments].[v_MeetingPayments] AS [MeetingPayment] " +
                                              "WHERE [MeetingPayment].[PayerId] = @PayerId AND [MeetingPayment].[MeetingId] = @MeetingId", new
            {
                query.MeetingId,
                query.PayerId
            });
        }
    }
}