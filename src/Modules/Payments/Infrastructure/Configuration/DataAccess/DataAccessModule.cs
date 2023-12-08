using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore;
using Microsoft.Extensions.Logging;
using SqlStreamStore;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.DataAccess
{
    internal class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;
        private readonly ILoggerFactory _loggerFactory;

        internal DataAccessModule(string databaseConnectionString, ILoggerFactory loggerFactory)
        {
            _databaseConnectionString = databaseConnectionString;
            _loggerFactory = loggerFactory;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.Register(a =>
            new MsSqlStreamStore(new MsSqlStreamStoreSettings(_databaseConnectionString)
            {
                Schema = DatabaseSchema.Name
            }))
                .As<IStreamStore>();

            builder.RegisterType<SqlStreamAggregateStore>()
                .As<IAggregateStore>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SqlServerCheckpointStore>()
                .As<ICheckpointStore>()
                .InstancePerLifetimeScope();

            var applicationAssembly = typeof(IProjector).Assembly;
            builder.RegisterAssemblyTypes(applicationAssembly)
                .Where(type => type.Name.EndsWith("Projector"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterType<SubscriptionsManager>()
                .As<SubscriptionsManager>()
                .SingleInstance();

            var infrastructureAssembly = ThisAssembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}