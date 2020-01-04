using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingAttendee
{
    internal class AddMeetingAttendeeCommandHandler : ICommandHandler<AddMeetingAttendeeCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;

        public AddMeetingAttendeeCommandHandler(
            IMemberContext memberContext, 
            IMeetingRepository meetingRepository, 
            IMeetingGroupRepository meetingGroupRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
            _meetingGroupRepository = meetingGroupRepository;
        }

        public async Task<Unit> Handle(AddMeetingAttendeeCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));
            
            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            meeting.AddAttendee(meetingGroup, _memberContext.MemberId, request.GuestsNumber);

            return Unit.Value;
        }
    }
}