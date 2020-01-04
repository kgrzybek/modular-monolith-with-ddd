using System;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Domain.UserRegistrations
{
    internal class UserRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<UserRegistration>
    {
        public void Configure(EntityTypeBuilder<UserRegistration> builder)
        {
            builder.ToTable("UserRegistrations", "users");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_login").HasColumnName("Login");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_password").HasColumnName("Password");
            builder.Property<string>("_firstName").HasColumnName("FirstName");
            builder.Property<string>("_lastName").HasColumnName("LastName");
            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<DateTime>("_registerDate").HasColumnName("RegisterDate");
            builder.Property<DateTime?>("_confirmedDate").HasColumnName("ConfirmedDate");

            builder.OwnsOne<UserRegistrationStatus>("_status", b =>
                {
                    b.Property(x => x.Value).HasColumnName("StatusCode");
                });
        }
    }
}
