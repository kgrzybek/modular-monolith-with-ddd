using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings
{
    internal class MarkMeetingAttendeeFeeAsPayedCommandHandler : ICommandHandler<MarkMeetingAttendeeFeeAsPayedCommand>
    {
        private readonly IMeetingRepository _meetingRepository;

        public MarkMeetingAttendeeFeeAsPayedCommandHandler(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task Handle(MarkMeetingAttendeeFeeAsPayedCommand command, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(command.MeetingId));

            meeting.MarkAttendeeFeeAsPayed(new MemberId(command.MemberId));
        }
    }
}