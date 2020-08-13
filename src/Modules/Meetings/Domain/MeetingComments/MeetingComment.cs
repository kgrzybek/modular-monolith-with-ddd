using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments
{
    public class MeetingComment : Entity, IAggregateRoot
    {
        public MeetingCommentId Id { get; }

        private MeetingId _meetingId;

        private MemberId _authorId;

        private MeetingCommentId? _inReplyToCommentId;

        private string _comment;

        private DateTime _createDate;

        private DateTime? _editDate;

        private MeetingComment(MeetingId meetingId, MemberId authorId, string comment, MeetingCommentId? inReplyToCommentId)
        {
            this.CheckRule(new CommentTextMustBeProvidedRule(comment));

            this.Id = new MeetingCommentId(Guid.NewGuid());
            _meetingId = meetingId;
            _authorId = authorId;
            _comment = comment;

            _inReplyToCommentId = inReplyToCommentId;

            _createDate = SystemClock.Now;
            _editDate = null;

            this.AddDomainEvent(new MeetingCommentCreatedDomainEvent(Id));
        }

        private MeetingComment()
        {
            // Only for EF.
        }

        public void Edit(MemberId editorId, string editedComment)
        {
            this.CheckRule(new CommentTextMustBeProvidedRule(editedComment));
            this.CheckRule(new MeetingCommentCanBeEditedOnlyByAuthor(this._authorId, editorId));

            _comment = editedComment;
            _editDate = SystemClock.Now;

            this.AddDomainEvent(new MeetingCommentEditedDomainEvent(this.Id, editedComment));
        }

        internal static MeetingComment Create(MeetingId meetingId, MemberId authorId, string comment)
            => new MeetingComment(meetingId, authorId, comment, inReplyToCommentId: null);
    }
}