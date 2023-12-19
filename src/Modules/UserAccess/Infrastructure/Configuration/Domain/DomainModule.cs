using Autofac;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.UserRegistrations;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure.Configuration.Domain
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