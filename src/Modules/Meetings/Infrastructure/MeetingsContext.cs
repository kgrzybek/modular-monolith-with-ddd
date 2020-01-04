using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Outbox;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure
{
    public class MeetingsContext : DbContext
    {
        public DbSet<MeetingGroup> MeetingGroups { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingGroupProposal> MeetingGroupProposals { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }
        public DbSet<Member> Members { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public MeetingsContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          //  optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MeetingGroupsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeetingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeetingGroupProposalEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MemberEntityTypeConfiguration());
        }
    }
}