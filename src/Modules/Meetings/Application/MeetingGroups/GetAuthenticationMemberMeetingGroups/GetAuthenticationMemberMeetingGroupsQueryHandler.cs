﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAuthenticationMemberMeetingGroups
{
    internal class GetAuthenticationMemberMeetingGroupsQueryHandler :
        IQueryHandler<GetAuthenticationMemberMeetingGroupsQuery, List<MemberMeetingGroupDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetAuthenticationMemberMeetingGroupsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory, 
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<List<MemberMeetingGroupDto>> Handle(
            GetAuthenticationMemberMeetingGroupsQuery query,
            CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql = "SELECT " +
                      $"[MemberMeetingGroup].[Id] AS [{nameof(MemberMeetingGroupDto.Id)}], " +
                      $"[MemberMeetingGroup].[Name] AS [{nameof(MemberMeetingGroupDto.Name)}], " +
                      $"[MemberMeetingGroup].[Description] AS [{nameof(MemberMeetingGroupDto.Description)}], " +
                      $"[MemberMeetingGroup].[LocationCountryCode] AS [{nameof(MemberMeetingGroupDto.LocationCountryCode)}], " +
                      $"[MemberMeetingGroup].[LocationCity] AS [{nameof(MemberMeetingGroupDto.LocationCity)}], " +
                      $"[MemberMeetingGroup].[MemberId] AS [{nameof(MemberMeetingGroupDto.MemberId)}], " +
                      $"[MemberMeetingGroup].[RoleCode] AS [{nameof(MemberMeetingGroupDto.RoleCode)}] " +
                      "FROM [meetings].[v_MemberMeetingGroups] AS [MemberMeetingGroup] " +
                      "WHERE [MemberMeetingGroup].MemberId = @MemberId AND [MemberMeetingGroup].[IsActive] = 1";

            var meetingGroups = await connection.QueryAsync<MemberMeetingGroupDto>(
                sql,
                new
                {
                    MemberId = _executionContextAccessor.UserId
                });

            return meetingGroups.AsList();
        }
    }
}