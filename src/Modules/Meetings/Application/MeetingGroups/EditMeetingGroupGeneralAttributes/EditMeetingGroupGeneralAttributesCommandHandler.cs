using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.EditMeetingGroupGeneralAttributes
{
    internal class EditMeetingGroupGeneralAttributesCommandHandler : ICommandHandler<EditMeetingGroupGeneralAttributesCommand>
    {
        private readonly IMemberContext _memberContext;
        private readonly IMeetingGroupRepository _meetingGroupRepository;

        internal EditMeetingGroupGeneralAttributesCommandHandler(IMemberContext memberContext, IMeetingGroupRepository meetingGroupRepository)
        {
            _memberContext = memberContext;
            _meetingGroupRepository = meetingGroupRepository;
        }

        public async Task<Unit> Handle(EditMeetingGroupGeneralAttributesCommand request, CancellationToken cancellationToken)
        {
            MeetingGroup meetingGroup =
                await _meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

            meetingGroup.EditGeneralAttributes(request.Name, request.Description, MeetingGroupLocation.CreateNew(request.LocationCity, request.LocationCountry));

            await _meetingGroupRepository.Commit();

            return Unit.Value;
        }
    }
}