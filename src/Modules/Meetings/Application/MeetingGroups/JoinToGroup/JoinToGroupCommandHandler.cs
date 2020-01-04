using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.JoinToGroup
{
    internal class JoinToGroupCommandHandler : ICommandHandler<JoinToGroupCommand>
    {
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMemberContext _memberContext;

        internal JoinToGroupCommandHandler(
            IMeetingGroupRepository meetingGroupRepository, 
            IMemberContext memberContext)
        {
            _meetingGroupRepository = meetingGroupRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(JoinToGroupCommand request, CancellationToken cancellationToken)
        {
            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

            meetingGroup.JoinToGroupMember(_memberContext.MemberId);

            return Unit.Value;
        }
    }
}