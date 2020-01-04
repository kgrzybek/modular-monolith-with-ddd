using Autofac;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Authentication
{
    internal class AuthenticationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemberContext>()
                .As<IMemberContext>()
                .InstancePerLifetimeScope();
        }
    }
}