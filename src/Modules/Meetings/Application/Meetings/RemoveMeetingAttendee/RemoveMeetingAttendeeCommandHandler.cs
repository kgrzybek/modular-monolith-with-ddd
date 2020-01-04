using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.RemoveMeetingAttendee
{
    internal class RemoveMeetingAttendeeCommandHandler : ICommandHandler<RemoveMeetingAttendeeCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMemberContext _memberContext;

        internal RemoveMeetingAttendeeCommandHandler(IMeetingRepository meetingRepository, IMemberContext memberContext)
        {
            _meetingRepository = meetingRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(RemoveMeetingAttendeeCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

            meeting.RemoveAttendee(new MemberId(request.AttendeeId), _memberContext.MemberId, request.RemovingReason);

            return Unit.Value;
        }
    }
}