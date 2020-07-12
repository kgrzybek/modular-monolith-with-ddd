using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings
{
    public class MarkMeetingAttendeeFeeAsPayedCommand : InternalCommandBase
    {
        public MarkMeetingAttendeeFeeAsPayedCommand(Guid id, Guid memberId, Guid meetingId) : base(id)
        {
            MemberId = memberId;

            MeetingId = meetingId;
        }

        public Guid MemberId { get; }

        public Guid MeetingId { get; }
    }
}