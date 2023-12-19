using Autofac;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration
{
    /// <summary>
    /// Represents the composition root for the Administration module.
    /// </summary>
    internal static class AdministrationCompositionRoot
    {
        private static IContainer _container;

        /// <summary>
        /// Sets the container for the composition root.
        /// </summary>
        /// <param name="container">The container to be set.</param>
        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Begins a new lifetime scope using the configured container.
        /// </summary>
        /// <returns>The newly created lifetime scope.</returns>
        public static ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }
    }
}