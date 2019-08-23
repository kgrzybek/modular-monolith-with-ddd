using Autofac;
using CompanyName.MyMeetings.Modules.Administration.Application.Users;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Authentication
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