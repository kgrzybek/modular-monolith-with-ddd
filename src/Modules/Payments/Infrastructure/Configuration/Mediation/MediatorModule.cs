﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using CompanyName.MyMeetings.BuildingBlocks.Application.Configuration.Commands;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Mediation
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceProviderWrapper>()
            .As<IServiceProvider>()
            .InstancePerDependency()
            .IfNotRegistered(typeof(IServiceProvider));

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var mediatorOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>),
                typeof(IRequestPreProcessor<>),
                typeof(IRequestHandler<>),
                typeof(IStreamRequestHandler<,>),
                typeof(IRequestPostProcessor<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(ICommandHandler<>),
                typeof(ICommandHandler<,>),
            };
            builder.RegisterSource(new ScopedContravariantRegistrationSource(
                mediatorOpenTypes));
            foreach (var mediatorOpenType in mediatorOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(Assemblies.Application, ThisAssembly)
                    .AsClosedTypesOf(mediatorOpenType)
                    .AsImplementedInterfaces()
                    .FindConstructorsWith(new AllConstructorFinder());
            }

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }

        private class ScopedContravariantRegistrationSource : IRegistrationSource
        {
            private readonly ContravariantRegistrationSource _source = new();
            private readonly List<Type> _types = new();

            public ScopedContravariantRegistrationSource(params Type[] types)
            {
                ArgumentNullException.ThrowIfNull(types);

                if (!types.All(x => x.IsGenericTypeDefinition))
                {
                    throw new ArgumentException("Supplied types should be generic type definitions");
                }

                _types.AddRange(types);
            }

            public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
            {
                var components = _source.RegistrationsFor(service, registrationAccessor);
                foreach (var c in components)
                {
                    var defs = c.Target.Services
                        .OfType<TypedService>()
                        .Select(x => x.ServiceType.GetGenericTypeDefinition());

                    if (defs.Any(_types.Contains))
                    {
                        yield return c;
                    }
                }
            }

            public bool IsAdapterForIndividualComponents => _source.IsAdapterForIndividualComponents;
        }
    }
}