using Autofac;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace CompanyName.MyMeetings.API.Modules.UserAccess
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