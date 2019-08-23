using FluentValidation;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup
{
    public class ProposeMeetingGroupCommandValidator : AbstractValidator<ProposeMeetingGroupCommand>
    {
        public ProposeMeetingGroupCommandValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage("Meeting group name cannot be empty");
        }
    }
}