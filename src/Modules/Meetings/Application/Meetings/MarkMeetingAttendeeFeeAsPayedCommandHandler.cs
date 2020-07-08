using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings
{
    public class MarkMeetingAttendeeFeeAsPayedCommandHandler : ICommandHandler<MarkMeetingAttendeeFeeAsPayedCommand>
    {
        private readonly IMeetingRepository _meetingRepository;

        public MarkMeetingAttendeeFeeAsPayedCommandHandler(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<Unit> Handle(MarkMeetingAttendeeFeeAsPayedCommand command, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(command.MeetingId));

            meeting.MarkAttendeeFeeAsPayed(new MemberId(command.MemberId));

            return Unit.Value;
        }
    }
}