using System.Collections.Generic;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups
{
    public class GetAllMeetingGroupsQuery : IQuery<List<MeetingGroupDto>>
    {
        
    }
}