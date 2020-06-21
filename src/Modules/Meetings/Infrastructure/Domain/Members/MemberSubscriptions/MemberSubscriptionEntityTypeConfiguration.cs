using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members.MemberSubscriptions
{
    internal class MemberSubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<MemberSubscription>
    {
        public void Configure(EntityTypeBuilder<MemberSubscription> builder)
        {
            builder.ToTable("MemberSubscriptions", "meetings");

            builder.HasKey(x => x.Id);

            builder.Property("_expirationDate").HasColumnName("ExpirationDate");
        }
    }
}
