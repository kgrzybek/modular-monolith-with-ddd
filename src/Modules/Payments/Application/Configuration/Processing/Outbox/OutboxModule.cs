using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Outbox;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Outbox;
using Module = Autofac.Module;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing.Outbox
{
    internal class OutboxModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<OutboxAccessor>()
                .As<IOutbox>()
                .FindConstructorsWith(new AllConstructorFinder())
                .InstancePerLifetimeScope();
        }
    }
}