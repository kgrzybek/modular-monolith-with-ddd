using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroups
{
    internal class MeetingGroupsEntityTypeConfiguration : IEntityTypeConfiguration<MeetingGroup>
    {
        public void Configure(EntityTypeBuilder<MeetingGroup> builder)
        {
            builder.ToTable("MeetingGroups", "meetings");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<string>("_description").HasColumnName("Description");
            builder.Property<MemberId>("_creatorId").HasColumnName("CreatorId");
            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");
            builder.Property<DateTime?>("_paymentDateTo").HasColumnName("PaymentDateTo");

            builder.OwnsOne<MeetingGroupLocation>("_location", b =>
            {
                b.Property(p => p.City).HasColumnName("LocationCity");
                b.Property(p => p.CountryCode).HasColumnName("LocationCountryCode");
            });

            builder.OwnsMany<MeetingGroupMember>("_members", y =>
            {
                y.WithOwner().HasForeignKey("MeetingGroupId");
                y.ToTable("MeetingGroupMembers", "meetings");
                y.Property<MemberId>("MemberId");
                y.Property<MeetingGroupId>("MeetingGroupId");
                y.Property<DateTime>("JoinedDate").HasColumnName("JoinedDate");
                y.HasKey("MemberId", "MeetingGroupId", "JoinedDate");

                y.Property<DateTime?>("_leaveDate").HasColumnName("LeaveDate");

                y.Property<bool>("_isActive").HasColumnName("IsActive");

                y.OwnsOne<MeetingGroupMemberRole>("_role", b =>
                {
                    b.Property<string>(x => x.Value).HasColumnName("RoleCode");
                });
            });
        }
    }
}
