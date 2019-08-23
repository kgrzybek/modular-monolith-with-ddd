using Autofac;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.API.Modules.Administration
{
    internal class AdministrationAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdministrationModule>()
                .As<IAdministrationModule>()
                .InstancePerLifetimeScope();
        }
    }
}