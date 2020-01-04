using Autofac;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration
{
    internal static class MeetingsCompositionRoot
    {
        private static IContainer _container;

        internal static void SetContainer(IContainer container)
        {
            _container = container;
        }

        internal static ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }
    }
}