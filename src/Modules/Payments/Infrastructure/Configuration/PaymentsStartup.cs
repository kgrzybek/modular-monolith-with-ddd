using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeePaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Authentication;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.DataAccess;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Email;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.EventsBus;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Logging;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Mediation;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Quartz;
using ILogger = Serilog.ILogger;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration
{
    public class PaymentsStartup
    {
        private static IContainer _container;

        private static SubscriptionsManager _subscriptionsManager;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            IEventsBus eventsBus,
            bool runQuartz = true,
            long? internalProcessingPoolingInterval = null)
        {
            var moduleLogger = logger.ForContext("Module", "Payments");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger, emailsConfiguration, eventsBus, runQuartz);

            if (runQuartz)
            {
                QuartzStartup.Initialize(moduleLogger, internalProcessingPoolingInterval);
            }

            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop()
        {
            _subscriptionsManager.Stop();
            QuartzStartup.StopQuartz();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            IEventsBus eventsBus,
            bool runQuartz = true)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            var loggerFactory = new Serilog.Extensions.Logging.SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));

            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EmailModule(emailsConfiguration));
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new AuthenticationModule());

            BiDictionary<string, Type> domainNotificationsMap = new BiDictionary<string, Type>();
            domainNotificationsMap.Add("MeetingFeePaidNotification", typeof(MeetingFeePaidNotification));
            domainNotificationsMap.Add("MeetingFeePaymentPaidNotification", typeof(MeetingFeePaymentPaidNotification));
            domainNotificationsMap.Add("SubscriptionCreatedNotification", typeof(SubscriptionCreatedNotification));
            domainNotificationsMap.Add("SubscriptionPaymentPaidNotification", typeof(SubscriptionPaymentPaidNotification));
            domainNotificationsMap.Add("SubscriptionRenewalPaymentPaidNotification", typeof(SubscriptionRenewalPaymentPaidNotification));
            domainNotificationsMap.Add("SubscriptionRenewedNotification", typeof(SubscriptionRenewedNotification));

            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            if (runQuartz)
            {
                containerBuilder.RegisterModule(new QuartzModule());
            }

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            PaymentsCompositionRoot.SetContainer(_container);

            RunEventsProjectors();
        }

        private static void RunEventsProjectors()
        {
            _subscriptionsManager = _container.Resolve<SubscriptionsManager>();

            _subscriptionsManager.Start();
        }
    }
}