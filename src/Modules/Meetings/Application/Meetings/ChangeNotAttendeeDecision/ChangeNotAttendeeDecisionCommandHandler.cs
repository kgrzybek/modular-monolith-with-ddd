using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.ChangeNotAttendeeDecision
{
    internal class ChangeNotAttendeeDecisionCommandHandler : ICommandHandler<ChangeNotAttendeeDecisionCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;

        public ChangeNotAttendeeDecisionCommandHandler(IMemberContext memberContext, IMeetingRepository meetingRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
        }

        public async Task<Unit> Handle(ChangeNotAttendeeDecisionCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

            meeting.ChangeNotAttendeeDecision(_memberContext.MemberId);

            return Unit.Value;
        }
    }
}