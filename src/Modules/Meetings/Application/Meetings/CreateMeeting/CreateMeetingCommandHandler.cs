using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.CreateMeeting
{
    internal class CreateMeetingCommandHandler : ICommandHandler<CreateMeetingCommand, Guid>
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

        public async Task<Guid> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

            var hostsMembersIds = request.HostMemberIds.Select(x => new MemberId(x)).ToList();

            var meeting = meetingGroup.CreateMeeting(
                request.Title,
                MeetingTerm.CreateNewBetweenDates(request.TermStartDate, request.TermStartDate),
                request.Description,
                MeetingLocation.CreateNew(request.MeetingLocationName, request.MeetingLocationAddress, request.MeetingLocationPostalCode, request.MeetingLocationCity),
                request.AttendeesLimit,
                request.GuestsLimit,
                Term.CreateNewBetweenDates(request.RSVPTermStartDate, request.RSVPTermEndDate),
                request.EventFeeValue.HasValue ? MoneyValue.Of(request.EventFeeValue.Value, request.EventFeeCurrency) : MoneyValue.Undefined,
                hostsMembersIds,
                _memberContext.MemberId);

            await _meetingRepository.AddAsync(meeting);

            return meeting.Id.Value;
        }
    }
}