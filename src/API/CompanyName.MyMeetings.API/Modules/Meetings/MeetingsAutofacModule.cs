using Autofac;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure;

namespace CompanyName.MyMeetings.API.Modules.Meetings
{
    public class MeetingsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MeetingsModule>()
                .As<IMeetingsModule>()
                .InstancePerLifetimeScope();
        }
    }
}