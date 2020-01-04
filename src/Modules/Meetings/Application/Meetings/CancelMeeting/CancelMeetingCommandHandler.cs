using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.CancelMeeting
{
    internal class CancelMeetingCommandHandler : ICommandHandler<CancelMeetingCommand>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMemberContext _memberContext;

        internal CancelMeetingCommandHandler(IMeetingRepository meetingRepository, IMemberContext memberContext)
        {
            _meetingRepository = meetingRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(CancelMeetingCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

            meeting.Cancel(_memberContext.MemberId);

            return Unit.Value;
        }
    }
}