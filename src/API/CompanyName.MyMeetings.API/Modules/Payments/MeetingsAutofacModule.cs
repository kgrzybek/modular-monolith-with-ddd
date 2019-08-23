using Autofac;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.API.Modules.Payments
{
    public class PaymentsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentsModule>()
                .As<IPaymentsModule>()
                .InstancePerLifetimeScope();
        }
    }
}