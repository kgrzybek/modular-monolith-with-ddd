using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.Members;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Domain.Members;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure
{
    /// <summary>
    /// Represents the database context for the Administration module.
    /// </summary>
    public class AdministrationContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="InternalCommand"/> entities.
        /// </summary>
        public DbSet<InternalCommand> InternalCommands { get; set; }

        /// <summary>
        /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="MeetingGroupProposal"/> entities.
        /// </summary>
        internal DbSet<MeetingGroupProposal> MeetingGroupProposals { get; set; }

        /// <summary>
        /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="OutboxMessage"/> entities.
        /// </summary>
        internal DbSet<OutboxMessage> OutboxMessages { get; set; }

        /// <summary>
        /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="Member"/> entities.
        /// </summary>
        internal DbSet<Member> Members { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrationContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public AdministrationContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Override method called when the model for a derived context has been initialized.
        /// Configures the model using the specified modelBuilder.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MeetingGroupProposalEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MemberEntityTypeConfiguration());
        }
    }
}