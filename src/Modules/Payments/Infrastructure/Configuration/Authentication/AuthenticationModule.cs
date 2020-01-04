using Autofac;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Authentication
{
    internal class AuthenticationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PayerContext>()
                .As<IPayerContext>()
                .InstancePerLifetimeScope();
        }
    }
}