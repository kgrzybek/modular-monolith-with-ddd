using Autofac;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration
{
    internal static class RegistrationsCompositionRoot
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