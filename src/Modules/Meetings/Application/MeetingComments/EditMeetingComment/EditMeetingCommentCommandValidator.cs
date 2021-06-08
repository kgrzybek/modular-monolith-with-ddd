using FluentValidation;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment
{
    internal class EditMeetingCommentCommandValidator : AbstractValidator<EditMeetingCommentCommand>
    {
        public EditMeetingCommentCommandValidator()
        {
            RuleFor(c => c.EditedComment).NotNull().NotEmpty()
                .WithMessage("Comment cannot be null or empty.");
        }
    }
}