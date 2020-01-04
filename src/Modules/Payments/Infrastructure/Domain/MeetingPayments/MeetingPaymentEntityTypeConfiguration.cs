using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Domain.MeetingPayments
{
    internal class MeetingPaymentEntityTypeConfiguration : IEntityTypeConfiguration<MeetingPayment>
    {
        public void Configure(EntityTypeBuilder<MeetingPayment> builder)
        {
            builder.ToTable("MeetingPayments", "payments");

            builder.HasKey(x => new { x.PayerId, x.MeetingId });

            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");
            builder.Property<DateTime?>("_paymentDate").HasColumnName("PaymentDate");

            builder.OwnsOne<MoneyValue>("_fee", b =>
            {
                b.Property(p => p.Value).HasColumnName("FeeValue");
                b.Property(p => p.Currency).HasColumnName("FeeCurrency");
            });
        }
    }
}