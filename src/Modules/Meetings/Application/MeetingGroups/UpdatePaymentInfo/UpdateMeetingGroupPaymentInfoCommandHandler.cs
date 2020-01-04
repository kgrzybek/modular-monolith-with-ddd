using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.UpdatePaymentInfo
{
    internal class UpdateMeetingGroupPaymentInfoCommandHandler : ICommandHandler<UpdateMeetingGroupPaymentInfoCommand>
    {
        private readonly IMeetingGroupRepository _meetingGroupRepository;

        internal UpdateMeetingGroupPaymentInfoCommandHandler(IMeetingGroupRepository meetingGroupRepository)
        {
            _meetingGroupRepository = meetingGroupRepository;
        }

        public async Task<Unit> Handle(UpdateMeetingGroupPaymentInfoCommand request, CancellationToken cancellationToken)
        {
            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

            meetingGroup.UpdatePaymentInfo(request.DateTo);

            return Unit.Value;
        }
    }
}