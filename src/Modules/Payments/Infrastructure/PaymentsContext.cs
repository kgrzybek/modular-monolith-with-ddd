using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Outbox;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Domain.MeetingGroupPaymentRegisters;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure
{
    public class PaymentsContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        internal DbSet<MeetingGroupPaymentRegister> MeetingGroupPaymentRegisters { get; set; }
        internal DbSet<MeetingPayment> MeetingPayments { get; set; }

        internal DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }
        internal DbSet<Payer> Payers { get; set; }

        public PaymentsContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeetingGroupPaymentRegisterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PayerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeetingPaymentEntityTypeConfiguration());
        }
    }
}