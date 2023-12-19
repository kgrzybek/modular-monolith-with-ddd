using Autofac;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity
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