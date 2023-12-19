using Autofac;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration;

internal static class UserAccessCompositionRoot
{
    private static IContainer? _container;

    internal static void SetContainer(IContainer container)
    {
        _container = container;
    }

    internal static ILifetimeScope BeginLifetimeScope()
    {
        if (_container is null)
        {
            throw new Exception("Container has not been initialised. Call SetContainer(instance) before using it.");
        }

        return _container.BeginLifetimeScope();
    }
}