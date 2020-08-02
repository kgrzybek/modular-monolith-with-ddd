using FluentValidation;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment
{
    public class EditMeetingCommentCommandValidator : AbstractValidator<EditMeetingCommentCommand>
    {
        public EditMeetingCommentCommandValidator()
        {
            RuleFor(c => c.EditedComment).NotNull().NotEmpty()
                .WithMessage("Comment cannot be null or empty.");
        }
    }
}