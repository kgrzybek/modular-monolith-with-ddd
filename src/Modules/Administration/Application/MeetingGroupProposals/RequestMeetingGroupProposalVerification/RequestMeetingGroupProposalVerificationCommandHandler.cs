using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification
{
    /// <summary>
    /// Handles the command for requesting verification of a meeting group proposal.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RequestMeetingGroupProposalVerificationCommandHandler"/> class.
    /// </remarks>
    /// <param name="meetingGroupProposalRepository">The meeting group proposal repository.</param>
    internal class RequestMeetingGroupProposalVerificationCommandHandler(IMeetingGroupProposalRepository meetingGroupProposalRepository) :
        ICommandHandler<RequestMeetingGroupProposalVerificationCommand, Guid>
    {
        private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository = meetingGroupProposalRepository;

        /// <summary>
        /// Handles the <see cref="RequestMeetingGroupProposalVerificationCommand"/> to verify a meeting group proposal.
        /// </summary>
        /// <param name="request">The command containing the details of the meeting group proposal to be verified.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ID of the verified meeting group proposal.</returns>
        public async Task<Guid> Handle(RequestMeetingGroupProposalVerificationCommand request, CancellationToken cancellationToken)
        {
            var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
                request.MeetingGroupProposalId,
                request.Name,
                request.Description,
                MeetingGroupLocation.Create(request.LocationCity, request.LocationCountryCode),
                new UserId(request.ProposalUserId),
                request.ProposalDate);

            await _meetingGroupProposalRepository.AddAsync(meetingGroupProposal);

            return meetingGroupProposal.Id.Value;
        }
    }
}