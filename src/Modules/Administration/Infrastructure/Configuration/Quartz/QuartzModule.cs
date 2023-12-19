using Autofac;
using Quartz;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Quartz
{
    /// <summary>
    /// Represents a module for configuring Quartz <see cref="IJob"/> in the Administration module.
    /// </summary>
    public class QuartzModule : Autofac.Module
    {
        /// <summary>
        /// Loads the Quartz module into the Autofac container.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
        }
    }
}