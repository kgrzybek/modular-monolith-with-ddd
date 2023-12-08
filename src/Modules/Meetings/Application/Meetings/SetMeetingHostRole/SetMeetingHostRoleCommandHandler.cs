﻿using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingHostRole
{
    internal class SetMeetingHostRoleCommandHandler : ICommandHandler<SetMeetingHostRoleCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;

        internal SetMeetingHostRoleCommandHandler(
            IMemberContext memberContext,
            IMeetingRepository meetingRepository,
            IMeetingGroupRepository meetingGroupRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
            _meetingGroupRepository = meetingGroupRepository;
        }

        public async Task Handle(SetMeetingHostRoleCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            meeting.SetHostRole(meetingGroup, _memberContext.MemberId, new MemberId(request.MemberId));
        }
    }
}