using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventsAccessor>()
                .As<IDomainEventsAccessor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandsScheduler>()
                .As<ICommandsScheduler>()
                .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));

            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));

            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));

            builder.RegisterGenericDecorator(
                typeof(DomainEventsDispatcherNotificationHandlerDecorator<>),
                typeof(INotificationHandler<>));

            builder.RegisterAssemblyTypes(Assemblies.Application)
                .AsClosedTypesOf(typeof(IDomainEventNotification<>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}