using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Domain.Payers
{
    internal class PayerEntityTypeConfiguration : IEntityTypeConfiguration<Payer>
    {
        public void Configure(EntityTypeBuilder<Payer> builder)
        {
            builder.ToTable("Payers", "payments");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_login").HasColumnName("Login");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_firstName").HasColumnName("FirstName");
            builder.Property<string>("_lastName").HasColumnName("LastName");
            builder.Property<string>("_name").HasColumnName("Name");
        }
    }
}
