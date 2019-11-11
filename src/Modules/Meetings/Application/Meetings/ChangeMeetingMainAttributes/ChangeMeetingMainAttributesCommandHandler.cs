using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.ChangeMeetingMainAttributes
{
    public class ChangeMeetingMainAttributesCommandHandler : ICommandHandler<ChangeMeetingMainAttributesCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingRepository _meetingRepository;

        public ChangeMeetingMainAttributesCommandHandler(IMemberContext memberContext, IMeetingRepository meetingRepository)
        {
            _memberContext = memberContext;
            _meetingRepository = meetingRepository;
        }

        public async Task<Unit> Handle(ChangeMeetingMainAttributesCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

            meeting.ChangeMainAttributes(request.Title,
                new MeetingTerm(request.TermStartDate, request.TermStartDate), 
                request.Description,
                new MeetingLocation(request.MeetingLocationName, request.MeetingLocationAddress, request.MeetingLocationPostalCode, request.MeetingLocationCity),
                MeetingLimits.Create(request.AttendeesLimit, request.GuestsLimit), 
                new Term(request.RSVPTermStartDate, request.RSVPTermEndDate),
                new MoneyValue(request.EventFeeValue, request.EventFeeCurrency),
                _memberContext.MemberId);

            return Unit.Value;
        }
    }
}