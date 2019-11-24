using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.MeetingGroupPaymentRegisters
{
    internal class MeetingGroupPaymentRegisterEntityTypeConfiguration : IEntityTypeConfiguration<MeetingGroupPaymentRegister>
    {
        public void Configure(EntityTypeBuilder<MeetingGroupPaymentRegister> builder)
        {
            builder.ToTable("MeetingGroupPaymentRegisters", "payments");

            builder.HasKey(x => x.Id);

            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");

            builder.OwnsMany<MeetingGroupPayment>("_payments", paymentBuilder =>
            {
                paymentBuilder.ToTable("MeetingGroupPayments", "payments");

                paymentBuilder.HasKey("Id");

                paymentBuilder.Property<MeetingGroupPaymentRegisterId>("MeetingGroupPaymentRegisterId");
                paymentBuilder.HasForeignKey("MeetingGroupPaymentRegisterId");
                paymentBuilder.Property<DateTime>("_date").HasColumnName("Date");
                paymentBuilder.Property<PayerId>("_payerId").HasColumnName("PayerId");

                paymentBuilder.OwnsOne<PaymentTerm>("_term", termBuilder =>
                {
                    termBuilder.Property(x => x.StartDate).HasColumnName("PaymentTermStartDate");
                    termBuilder.Property(x => x.EndDate).HasColumnName("PaymentTermEndDate");
                });
            });
        }
    }
}