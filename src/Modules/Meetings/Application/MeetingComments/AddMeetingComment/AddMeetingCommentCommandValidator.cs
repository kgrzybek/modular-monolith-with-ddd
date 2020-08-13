using FluentValidation;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment
{
    internal class AddMeetingCommentCommandValidator : AbstractValidator<AddMeetingCommentCommand>
    {
        public AddMeetingCommentCommandValidator()
        {
            this.RuleFor(c => c.MeetingId).NotEmpty()
                .WithMessage("Id of meeting member cannot be empty.");

            this.RuleFor(c => c.Comment).NotNull().NotEmpty()
                .WithMessage("Comment cannot be null or empty.");
        }
    }
}