using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingCommentingConfigurations
{
    public class MeetingCommentingConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<MeetingCommentingConfiguration>
    {
        public void Configure(EntityTypeBuilder<Modules.Meetings.Domain.MeetingCommentingConfigurations.MeetingCommentingConfiguration> builder)
        {
            builder.ToTable("MeetingCommentingConfigurations", "meetings");

            builder.HasKey(c => c.Id);
            builder.HasOne<Meeting>()
                .WithOne()
                .HasForeignKey(nameof(MeetingCommentingConfiguration), "_meetingId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property<MeetingId>("_meetingId").HasColumnName("MeetingId");
            builder.Property<bool>("_isCommentingEnabled").HasColumnName("IsCommentingEnabled");
        }
    }
}