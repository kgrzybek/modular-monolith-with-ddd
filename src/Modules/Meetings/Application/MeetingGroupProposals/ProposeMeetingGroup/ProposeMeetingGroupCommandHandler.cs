using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup
{
    internal class ProposeMeetingGroupCommandHandler : ICommandHandler<ProposeMeetingGroupCommand>
    {
        private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository;
        private readonly IMemberContext _memberContext;

        internal ProposeMeetingGroupCommandHandler(
            IMeetingGroupProposalRepository meetingGroupProposalRepository, 
            IMemberContext memberContext)
        {
            _meetingGroupProposalRepository = meetingGroupProposalRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(ProposeMeetingGroupCommand request, CancellationToken cancellationToken)
        {
            var meetingGroupProposal = MeetingGroupProposal.ProposeNew(
                request.Name, 
                request.Description,
                MeetingGroupLocation.CreateNew(request.LocationCity, request.LocationCountryCode), 
                _memberContext.MemberId);

            await _meetingGroupProposalRepository.AddAsync(meetingGroupProposal);

            return new Unit();
        }
    }
}