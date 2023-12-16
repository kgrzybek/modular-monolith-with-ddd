using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup
{
    internal class CreateNewMeetingGroupCommandHandler : ICommandHandler<CreateNewMeetingGroupCommand>
    {
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository;

        internal CreateNewMeetingGroupCommandHandler(
            IMeetingGroupRepository meetingGroupRepository,
            IMeetingGroupProposalRepository meetingGroupProposalRepository)
        {
            _meetingGroupRepository = meetingGroupRepository;
            _meetingGroupProposalRepository = meetingGroupProposalRepository;
        }

        public async Task Handle(CreateNewMeetingGroupCommand request, CancellationToken cancellationToken)
        {
            var meetingGroupProposal = await _meetingGroupProposalRepository.GetByIdAsync(request.MeetingGroupProposalId);

            var meetingGroup = meetingGroupProposal.CreateMeetingGroup();

            await _meetingGroupRepository.AddAsync(meetingGroup);
        }
    }
}