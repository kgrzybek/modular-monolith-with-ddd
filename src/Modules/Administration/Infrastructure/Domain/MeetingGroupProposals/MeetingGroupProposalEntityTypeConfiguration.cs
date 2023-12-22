using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Domain.MeetingGroupProposals
{
    internal class MeetingGroupProposalEntityTypeConfiguration : IEntityTypeConfiguration<MeetingGroupProposal>
    {
        public void Configure(EntityTypeBuilder<MeetingGroupProposal> builder)
        {
            builder.ToTable("MeetingGroupProposals", "administration");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<string>("_description").HasColumnName("Description");
            builder.Property<UserId>("_proposalUserId").HasColumnName("ProposalUserId");
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

            builder.OwnsOne<MeetingGroupProposalDecision>("_decision", b =>
            {
                b.Property(p => p.Code).HasColumnName("DecisionCode");
                b.Property(p => p.Date).HasColumnName("DecisionDate");
                b.Property(p => p.RejectReason).HasColumnName("DecisionRejectReason");
                b.Property(p => p.UserId).HasColumnName("DecisionUserId");
            });
        }
    }
}
