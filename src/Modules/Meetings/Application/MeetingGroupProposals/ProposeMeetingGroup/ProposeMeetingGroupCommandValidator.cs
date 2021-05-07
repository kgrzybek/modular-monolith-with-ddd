using FluentValidation;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup
{
    internal class ProposeMeetingGroupCommandValidator : AbstractValidator<ProposeMeetingGroupCommand>
    {
        public ProposeMeetingGroupCommandValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage("Meeting group name cannot be empty");
            this.RuleFor(x => x.LocationCity).NotEmpty().WithMessage("Meeting group city cannot be empty");
            this.RuleFor(x => x.LocationCountryCode).NotEmpty().WithMessage("Meeting country code cannot be empty");
        }
    }
}