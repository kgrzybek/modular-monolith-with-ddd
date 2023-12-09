using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingMemberCommentLikes
{
    public class MeetingMemberCommentLikeEntityTypeConfiguration : IEntityTypeConfiguration<MeetingMemberCommentLike>
    {
        public void Configure(EntityTypeBuilder<MeetingMemberCommentLike> builder)
        {
            builder.ToTable("MeetingMemberCommentLikes", "meetings");

            builder.HasKey(c => c.Id);

            builder.Property<MeetingCommentId>("_meetingCommentId").HasColumnName("MeetingCommentId");
            builder.Property<MemberId>("_memberId").HasColumnName("MemberId");

            builder.HasOne<Member>()
                .WithMany()
                .HasForeignKey("_memberId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<MeetingComment>()
                .WithMany()
                .HasForeignKey("_meetingCommentId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}