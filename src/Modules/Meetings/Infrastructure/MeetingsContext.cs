using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions;
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

        public DbSet<MemberSubscription> MemberSubscriptions { get; set; }

        public DbSet<MeetingComment> MeetingComments { get; set; }

        public DbSet<MeetingCommentingConfiguration> MeetingCommentingConfigurations { get; set; }

        public DbSet<MeetingMemberCommentLike> MeetingMemberCommentLikes { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public MeetingsContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}