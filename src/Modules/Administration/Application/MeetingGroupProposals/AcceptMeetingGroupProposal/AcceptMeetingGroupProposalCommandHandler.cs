using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    internal class AcceptMeetingGroupProposalCommandHandler : ICommandHandler<AcceptMeetingGroupProposalCommand>
    {
        private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository;
        private readonly IUserContext _userContext;

        internal AcceptMeetingGroupProposalCommandHandler(IMeetingGroupProposalRepository meetingGroupProposalRepository, IUserContext userContext)
        {
            _meetingGroupProposalRepository = meetingGroupProposalRepository;
            _userContext = userContext;
        }

        public async Task Handle(AcceptMeetingGroupProposalCommand request, CancellationToken cancellationToken)
        {
            var meetingGroupProposal = await _meetingGroupProposalRepository.GetByIdAsync(new MeetingGroupProposalId(request.MeetingGroupProposalId));

            meetingGroupProposal.Accept(_userContext.UserId);
        }
    }
}