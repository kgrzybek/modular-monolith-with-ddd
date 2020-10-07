using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.GetMeetingFees
{
    internal class GetMeetingFeesQueryHandler : IQueryHandler<GetMeetingFeesQuery, List<MeetingFeeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingFeesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingFeeDto>> Handle(GetMeetingFeesQuery query, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            var meetingFees = await connection.QueryAsync<MeetingFeeDto>(
                "SELECT" +
                $"[MeetingFee].MeetingFeeId AS [{nameof(MeetingFeeDto.MeetingFeeId)}], " +
                $"[MeetingFee].PayerId AS [{nameof(MeetingFeeDto.PayerId)}], " +
                $"[MeetingFee].FeeCurrency AS [{nameof(MeetingFeeDto.FeeCurrency)}], " +
                $"[MeetingFee].FeeValue AS [{nameof(MeetingFeeDto.FeeValue)}], " +
                $"[MeetingFee].MeetingId AS [{nameof(MeetingFeeDto.MeetingId)}], " +
                $"[MeetingFee].Status AS [{nameof(MeetingFeeDto.Status)}] " +
                "FROM [payments].[MeetingFees] AS [MeetingFee] " +
                "WHERE [MeetingFee].MeetingId = @MeetingId",
                new
                {
                    query.MeetingId
                });

            return meetingFees.AsList();
        }
    }
}