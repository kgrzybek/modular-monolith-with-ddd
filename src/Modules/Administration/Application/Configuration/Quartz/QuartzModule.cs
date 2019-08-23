using Autofac;
using Quartz;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Quartz
{
    public class QuartzModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
        }
    }
}