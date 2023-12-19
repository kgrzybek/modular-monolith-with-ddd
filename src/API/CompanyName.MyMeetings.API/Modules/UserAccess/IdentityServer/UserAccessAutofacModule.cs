using Autofac;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.IdentityServer
{
    public class UserAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAccessModule>()
                .As<IUserAccessModule>()
                .InstancePerLifetimeScope();
        }
    }
}