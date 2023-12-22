using System.Collections.Specialized;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing.Inbox;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing.InternalCommands;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Quartz.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        internal static void StopQuartz()
        {
            _scheduler.Shutdown();
        }

        internal static void Initialize(ILogger logger, long? internalProcessingPoolingInterval)
        {
            logger.Information("Quartz starting...");

            var schedulerConfiguration = new NameValueCollection();
            schedulerConfiguration.Add("quartz.scheduler.instanceName", "Meetings");

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            _scheduler.Start().GetAwaiter().GetResult();

            ScheduleProcessOutboxJob(_scheduler, internalProcessingPoolingInterval);

            ScheduleProcessInboxJob(_scheduler, internalProcessingPoolingInterval);

            ScheduleProcessInternalCommandsJob(_scheduler, internalProcessingPoolingInterval);

            ScheduleExpireSubscriptionsJob(_scheduler);

            ScheduleExpireSubscriptionPaymentsJob(_scheduler);

            logger.Information("Quartz started.");
        }

        private static void ScheduleExpireSubscriptionPaymentsJob(IScheduler scheduler)
        {
            var expireSubscriptionPaymentsJob = JobBuilder.Create<ExpireSubscriptionPaymentsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/59 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(expireSubscriptionPaymentsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
        }

        private static void ScheduleExpireSubscriptionsJob(IScheduler scheduler)
        {
            var expireSubscriptionsJob = JobBuilder.Create<ExpireSubscriptionsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/59 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(expireSubscriptionsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
        }

        private static void ScheduleProcessInternalCommandsJob(
            IScheduler scheduler,
            long? internalProcessingPoolingInterval)
        {
            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();

            ITrigger triggerCommandsProcessing;
            if (internalProcessingPoolingInterval.HasValue)
            {
                triggerCommandsProcessing =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithSimpleSchedule(x =>
                            x.WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value))
                                .RepeatForever())
                        .Build();
            }
            else
            {
                triggerCommandsProcessing =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithCronSchedule("0/2 * * ? * *")
                        .Build();
            }

            scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
        }

        private static void ScheduleProcessInboxJob(IScheduler scheduler, long? internalProcessingPoolingInterval)
        {
            var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();

            ITrigger processInboxTrigger;
            if (internalProcessingPoolingInterval.HasValue)
            {
                processInboxTrigger =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithSimpleSchedule(x =>
                            x.WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value))
                                .RepeatForever())
                        .Build();
            }
            else
            {
                processInboxTrigger =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithCronSchedule("0/2 * * ? * *")
                        .Build();
            }

            scheduler
                .ScheduleJob(processInboxJob, processInboxTrigger)
                .GetAwaiter().GetResult();
        }

        private static void ScheduleProcessOutboxJob(IScheduler scheduler, long? internalProcessingPoolingInterval)
        {
            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();

            ITrigger trigger;
            if (internalProcessingPoolingInterval.HasValue)
            {
                trigger =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithSimpleSchedule(x =>
                            x.WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value))
                                .RepeatForever())
                        .Build();
            }
            else
            {
                trigger =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithCronSchedule("0/2 * * ? * *")
                        .Build();
            }

            scheduler
                .ScheduleJob(processOutboxJob, trigger)
                .GetAwaiter().GetResult();
        }
    }
}