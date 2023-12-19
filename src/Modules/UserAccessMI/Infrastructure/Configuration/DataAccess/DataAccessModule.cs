using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.DataAccess;

internal class DataAccessModule : Autofac.Module
{
    private readonly string _databaseConnectionString;

    internal DataAccessModule(string databaseConnectionString)
    {
        _databaseConnectionString = databaseConnectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(x => new DatabaseConfiguration(_databaseConnectionString))
            .As<IDatabaseConfiguration>();

        builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

        builder
            .RegisterType<UserAccessContext>()
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();

        var infrastructureAssembly = typeof(UserAccessContext).Assembly;

        builder.RegisterAssemblyTypes(infrastructureAssembly)
            .Where(type => type.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .FindConstructorsWith(new AllConstructorFinder());
    }
}