using System.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees.Events;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.GetMeetingFees
{
    internal class MeetingFeesProjector : ProjectorBase, IProjector
    {
        private readonly IDbConnection _connection;

        public MeetingFeesProjector(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connection = sqlConnectionFactory.GetOpenConnection();
        }

        public async Task Project(IDomainEvent @event)
        {
            await When((dynamic)@event);
        }

        private async Task When(MeetingFeeCreatedDomainEvent meetingFeeCreated)
        {
            await _connection.ExecuteScalarAsync(
                "INSERT INTO payments.MeetingFees " +
                "([MeetingFeeId], [PayerId], [MeetingId], [FeeValue], [FeeCurrency], [Status]) " +
                "VALUES (@MeetingFeeId, @PayerId, @MeetingId, @FeeValue, @FeeCurrency, @Status)",
                new
                {
                    meetingFeeCreated.MeetingFeeId,
                    meetingFeeCreated.PayerId,
                    meetingFeeCreated.MeetingId,
                    meetingFeeCreated.FeeValue,
                    meetingFeeCreated.FeeCurrency,
                    meetingFeeCreated.Status
                });
        }

        private async Task When(MeetingFeePaidDomainEvent meetingFeePaid)
        {
            await UpdateStatus(meetingFeePaid.MeetingFeeId, meetingFeePaid.Status);
        }

        private async Task When(MeetingFeeExpiredDomainEvent meetingFeeExpired)
        {
            await UpdateStatus(meetingFeeExpired.MeetingFeeId, meetingFeeExpired.Status);
        }

        private async Task When(MeetingFeeCanceledDomainEvent meetingFeeCanceled)
        {
            await UpdateStatus(meetingFeeCanceled.MeetingFeeId, meetingFeeCanceled.Status);
        }

        private async Task UpdateStatus(Guid meetingFeeId, string status)
        {
            await _connection.ExecuteScalarAsync(
                "UPDATE payments.MeetingFees " +
                "SET " +
                "[Status] = @Status " +
                "WHERE [MeetingFeeId] = @MeetingFeeId",
                new
                {
                    meetingFeeId,
                    status
                });
        }
    }
}