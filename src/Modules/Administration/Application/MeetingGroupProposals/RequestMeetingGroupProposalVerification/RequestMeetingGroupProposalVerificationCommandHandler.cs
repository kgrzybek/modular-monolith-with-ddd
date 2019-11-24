using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification
{
    internal class RequestMeetingGroupProposalVerificationCommandHandler : 
        ICommandHandler<RequestMeetingGroupProposalVerificationCommand>
    {
        private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository;

        public RequestMeetingGroupProposalVerificationCommandHandler(IMeetingGroupProposalRepository meetingGroupProposalRepository)
        {
            _meetingGroupProposalRepository = meetingGroupProposalRepository;
        }

        public async Task<Unit> Handle(RequestMeetingGroupProposalVerificationCommand request, CancellationToken cancellationToken)
        {
            var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
                request.MeetingGroupProposalId,
                request.Name,
                request.Description,
                MeetingGroupLocation.Create(request.LocationCity, request.LocationCountryCode),
                new UserId(request.ProposalUserId),
                request.ProposalDate
            );

            await _meetingGroupProposalRepository.AddAsync(meetingGroupProposal);

            return Unit.Value;
        }
    }
}