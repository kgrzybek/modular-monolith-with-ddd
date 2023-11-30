using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    internal class AcceptMeetingGroupProposalCommandHandler : ICommandHandler<AcceptMeetingGroupProposalCommand>
    {
        private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository;

        public AcceptMeetingGroupProposalCommandHandler(IMeetingGroupProposalRepository meetingGroupProposalRepository)
        {
            _meetingGroupProposalRepository = meetingGroupProposalRepository;
        }

        public async Task Handle(AcceptMeetingGroupProposalCommand request, CancellationToken cancellationToken)
        {
            var meetingGroupProposal = await _meetingGroupProposalRepository.GetByIdAsync(new MeetingGroupProposalId(request.MeetingGroupProposalId));

            meetingGroupProposal.Accept();
        }
    }
}