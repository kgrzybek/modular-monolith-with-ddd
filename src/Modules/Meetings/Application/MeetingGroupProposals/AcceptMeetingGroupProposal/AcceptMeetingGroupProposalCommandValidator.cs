using FluentValidation;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    internal class AcceptMeetingGroupProposalCommandValidator : AbstractValidator<AcceptMeetingGroupProposalCommand>
    {
        public AcceptMeetingGroupProposalCommandValidator()
        {
            this.RuleFor(x => x.MeetingGroupProposalId).NotEmpty()
                .WithMessage("Id of meeting group proposal cannot be empty");
        }
    }
}