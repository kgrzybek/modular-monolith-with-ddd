using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroupProposals
{
    internal class MeetingGroupProposalEntityTypeConfiguration : IEntityTypeConfiguration<MeetingGroupProposal>
    {
        public void Configure(EntityTypeBuilder<MeetingGroupProposal> builder)
        {
            builder.ToTable("MeetingGroupProposals", "meetings");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<string>("_description").HasColumnName("Description");
            builder.Property<MemberId>("_proposalUserId").HasColumnName("ProposalUserId");
            builder.Property<DateTime>("_proposalDate").HasColumnName("ProposalDate");

            builder.OwnsOne<MeetingGroupLocation>("_location", b =>
            {
                b.Property(p => p.City).HasColumnName("LocationCity");
                b.Property(p => p.CountryCode).HasColumnName("LocationCountryCode");
            });

            builder.OwnsOne<MeetingGroupProposalStatus>("_status", b =>
            {
                b.Property(p => p.Value).HasColumnName("StatusCode");
            });
        }
    }
}
