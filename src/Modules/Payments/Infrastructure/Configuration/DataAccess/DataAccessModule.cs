using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

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

            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<PaymentsContext>();
                    dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);

                    dbContextOptionsBuilder
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                    return new PaymentsContext(dbContextOptionsBuilder.Options, _loggerFactory);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            var infrastructureAssembly = typeof(PaymentsContext).Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());;
        }
    }
}