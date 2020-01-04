using Autofac;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Authentication
{
    internal class AuthenticationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .InstancePerLifetimeScope();
        }
    }
}