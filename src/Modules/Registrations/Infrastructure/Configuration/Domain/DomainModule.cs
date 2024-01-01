using Autofac;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Domain
{
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersCounter>()
                .As<IUsersCounter>()
                .InstancePerLifetimeScope();
        }
    }
}