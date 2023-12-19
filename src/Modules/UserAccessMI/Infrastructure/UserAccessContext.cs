using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Domain.Configuration;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Outbox;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure;

public class UserAccessContext : IdentityDbContext<ApplicationUser, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly Serilog.ILogger? _logger;
    private readonly IDatabaseConfiguration _databaseConfiguration;

    public UserAccessContext(IDatabaseConfiguration databaseConfiguration, Serilog.ILogger logger)
    {
        _logger = logger;
        _databaseConfiguration = databaseConfiguration;
    }

    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = default!;

    public DbSet<UserRegistration> UserRegistrations { get; set; } = default!;

    public DbSet<OutboxMessage> OutboxMessages { get; set; } = default!;

    public DbSet<InternalCommand> InternalCommands { get; set; } = default!;

    /// <summary>
    /// Abstract away the configuration complexity by encapsulating the DbContext.
    /// </summary>
    /// <param name="optionsBuilder">Options builder instance.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_databaseConfiguration.ConnectionString);
        optionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

        if (_logger is not null)
        {
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }
        else
        {
            optionsBuilder.UseLoggerFactory(CreateEmptyLoggerFactory());
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityTypeConfiguration());
        builder.ApplyConfiguration(new IdentityRoleClaimEntityTypeConfiguration());
        builder.ApplyConfiguration(new IdentityUserClaimEntityTypeConfiguration());
        builder.ApplyConfiguration(new IdentityUserLoginEntityTypeConfiguration());
        builder.ApplyConfiguration(new IdentityUserRoleEntityTypeConfiguration());
        builder.ApplyConfiguration(new IdentityUserTokenEntityTypeConfiguration());
        builder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        builder.ApplyConfiguration(new UserRefreshTokenEntityTypeConfiguration());

        builder.ApplyConfiguration(new UserRegistrationEntityTypeConfiguration());
        builder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        builder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
    }

    private ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder => builder
            .AddSerilog(_logger));
    }

    private ILoggerFactory CreateEmptyLoggerFactory()
    {
        return LoggerFactory.Create(builder => builder
            .AddFilter((_, _) => false));
    }
}