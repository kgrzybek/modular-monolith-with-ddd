using Autofac;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MeetingCommentService>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}