﻿using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.Users;
using CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure.Domain.Users;
using CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure
{
    public class UserAccessContext : DbContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public UserAccessContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRegistrationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
        }
    }
}