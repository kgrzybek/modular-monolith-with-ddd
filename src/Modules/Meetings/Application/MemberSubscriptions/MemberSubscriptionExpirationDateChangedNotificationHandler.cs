using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Policies;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions
{
    public class MemberSubscriptionExpirationDateChangedNotificationHandler :
        INotificationHandler<MemberSubscriptionExpirationDateChangedNotification>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly ICommandsScheduler _commandsScheduler;

        public MemberSubscriptionExpirationDateChangedNotificationHandler(ISqlConnectionFactory sqlConnectionFactory, ICommandsScheduler commandsScheduler)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MemberSubscriptionExpirationDateChangedNotification notification, CancellationToken cancellationToken)
        {
            const string sql = $"""
                               SELECT 
                                   [MeetingGroupMember].MeetingGroupId AS [{nameof(MeetingGroupMemberResponse.MeetingGroupId)}], 
                                   [MeetingGroupMember].RoleCode AS [{nameof(MeetingGroupMemberResponse.RoleCode)}] 
                               FROM [meetings].[v_MeetingGroupMembers] AS [MeetingGroupMember] 
                               WHERE [MeetingGroupMember].MemberId = @MemberId
                               """;

            var connection = _sqlConnectionFactory.GetOpenConnection();

            var meetingGroupMembers = await connection.QueryAsync<MeetingGroupMemberResponse>(
                sql,
                new
                {
                    MemberId = notification.DomainEvent.MemberId.Value
                });

            var meetingGroupList = meetingGroupMembers.AsList();

            List<MeetingGroupMemberData> meetingGroups = meetingGroupList
                .Select(x =>
                    new MeetingGroupMemberData(
                        new MeetingGroupId(x.MeetingGroupId),
                        MeetingGroupMemberRole.Of(x.RoleCode)))
                .ToList();

            var meetingGroupsCoveredByMemberSubscription =
                MeetingGroupExpirationDatePolicy.GetMeetingGroupsCoveredByMemberSubscription(meetingGroups);

            foreach (var meetingGroup in meetingGroupsCoveredByMemberSubscription)
            {
                await _commandsScheduler.EnqueueAsync(new SetMeetingGroupExpirationDateCommand(
                    Guid.NewGuid(),
                    meetingGroup.Value,
                    notification.DomainEvent.ExpirationDate));
            }
        }

        private class MeetingGroupMemberResponse
        {
            public Guid MeetingGroupId { get; set; }

            public string RoleCode { get; set; }
        }
    }
}