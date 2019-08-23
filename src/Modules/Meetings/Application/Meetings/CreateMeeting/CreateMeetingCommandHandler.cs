using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.CreateMeeting
{
    internal class CreateMeetingCommandHandler : ICommandHandler<CreateMeetingCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;

        internal CreateMeetingCommandHandler(
            IMemberContext memberContext, 
            IMeetingRepository meetingRepository, 
            IMeetingGroupRepository meetingGroupRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
            _meetingGroupRepository = meetingGroupRepository;
        }

        public async Task<Unit> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

            var hostsMembersIds = request.HostMemberIds.Select(x => new MemberId(x)).ToList();

            var meeting = meetingGroup.CreateMeeting(
                request.Title,
                new MeetingTerm(request.TermStartDate, request.TermStartDate), 
                request.Description,
                new MeetingLocation(request.MeetingLocationName, request.MeetingLocationAddress, request.MeetingLocationPostalCode, request.MeetingLocationCity),
                request.AttendeesLimit,
                request.GuestsLimit,
                new Term(request.RSVPTermStartDate, request.RSVPTermEndDate),
                new MoneyValue(request.EventFeeValue, request.EventFeeCurrency),
                hostsMembersIds,
                _memberContext.MemberId);

            await _meetingRepository.AddAsync(meeting);

            return Unit.Value;
        }
    }
}