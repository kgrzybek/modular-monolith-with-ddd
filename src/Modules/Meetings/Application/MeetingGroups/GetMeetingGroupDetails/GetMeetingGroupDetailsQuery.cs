﻿using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails
{
    public class GetMeetingGroupDetailsQuery : QueryBase<MeetingGroupDetailsDto>
    {
        public GetMeetingGroupDetailsQuery(Guid meetingGroupId)
        {
            MeetingGroupId = meetingGroupId;
        }

        public Guid MeetingGroupId { get; }
    }
}