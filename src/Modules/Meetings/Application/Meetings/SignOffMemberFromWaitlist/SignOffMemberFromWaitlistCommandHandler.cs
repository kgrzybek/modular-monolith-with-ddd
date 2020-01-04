using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SignOffMemberFromWaitlist
{
    internal class SignOffMemberFromWaitlistCommandHandler : ICommandHandler<SignOffMemberFromWaitlistCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;

        public SignOffMemberFromWaitlistCommandHandler(IMemberContext memberContext, IMeetingRepository meetingRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
        }

        public async Task<Unit> Handle(SignOffMemberFromWaitlistCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

            meeting.SignOffMemberFromWaitlist(_memberContext.MemberId);

            return Unit.Value;
        }
    }
}