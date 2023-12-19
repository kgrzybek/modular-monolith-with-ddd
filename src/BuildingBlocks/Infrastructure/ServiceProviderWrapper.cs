using Autofac;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure
{
    /// <summary>
    /// Wraps an <see cref="ILifetimeScope"/> instance and implements the <see cref="IServiceProvider"/> interface.
    /// </summary>
    public class ServiceProviderWrapper : IServiceProvider
    {
        private readonly ILifetimeScope lifeTimeScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProviderWrapper"/> class.
        /// </summary>
        /// <param name="lifeTimeScope">The lifetime scope.</param>
        public ServiceProviderWrapper(ILifetimeScope lifeTimeScope)
        {
            this.lifeTimeScope = lifeTimeScope;
        }

        #nullable enable
        /// <inheritdoc/>
        public object? GetService(Type serviceType) => this.lifeTimeScope.ResolveOptional(serviceType);
    }
}
