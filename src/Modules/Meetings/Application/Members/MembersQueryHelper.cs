using System;
using System.Data;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members
{
    public class MembersQueryHelper
    {
        public static async Task<MemberDto> GetMember(MemberId memberId, IDbConnection connection)
        {
            return await connection.QuerySingleAsync<MemberDto>(
                "SELECT " +
                "[Member].Id, " +
                "[Member].[Name], " +
                "[Member].[Login], " +
                "[Member].[Email] " +
                "FROM [meetings].[v_Members] AS [Member] " +
                "WHERE [Member].[Id] = @Id",
                new
                {
                    Id = memberId.Value
                });
        }

        public static async Task<MeetingGroupMemberData> GetMeetingGroupMember(MemberId memberId, MeetingId meetingOfGroupId, IDbConnection connection)
        {
            var result = await connection.QuerySingleAsync<MeetingGroupMemberResponse>(
                "SELECT " +
                $"[MeetingGroupMember].{nameof(MeetingGroupMemberResponse.MeetingGroupId)}, " +
                $"[MeetingGroupMember].{nameof(MeetingGroupMemberResponse.MemberId)} " +
                "FROM [meetings].[v_MeetingGroupMembers] AS [MeetingGroupMember] " +
                "INNER JOIN [meetings].[Meetings] AS [Meeting] ON [Meeting].[MeetingGroupId] = [MeetingGroupMember].[MeetingGroupId] " +
                "WHERE [MeetingGroupMember].[MemberId] = @MemberId AND [Meeting].[Id] = @MeetingId",
                new
                {
                    MemberId = memberId.Value,
                    MeetingId = meetingOfGroupId.Value
                });

            return new MeetingGroupMemberData(
                new MeetingGroupId(result.MeetingGroupId),
                new MemberId(result.MemberId));
        }

        private class MeetingGroupMemberResponse
        {
            public Guid MeetingGroupId { get; set; }

            public Guid MemberId { get; set; }
        }
    }
}