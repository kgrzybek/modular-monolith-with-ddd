using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingComments
{
    public class MeetingCommentEntityTypeConfiguration : IEntityTypeConfiguration<MeetingComment>
    {
        public void Configure(EntityTypeBuilder<MeetingComment> builder)
        {
            builder.ToTable("MeetingComments", "meetings");

            builder.HasKey(c => c.Id);

            builder.Property<string>("_comment").HasColumnName("Comment");
            builder.Property<MeetingId>("_meetingId").HasColumnName("MeetingId");
            builder.Property<MemberId>("_authorId").HasColumnName("AuthorId");
            builder.Property<MeetingCommentId>("_inReplyToCommentId").HasColumnName("InReplyToCommentId");
            builder.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
            builder.Property<string>("_removedByReason").HasColumnName("RemovedByReason");
            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");
            builder.Property<DateTime?>("_editDate").HasColumnName("EditDate");
        }
    }
}