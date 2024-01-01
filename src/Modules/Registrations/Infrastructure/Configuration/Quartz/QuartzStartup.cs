using System.Collections.Specialized;
using CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing.Inbox;
using CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing.InternalCommands;
using CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing.Outbox;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        internal static void Initialize(ILogger logger, long? internalProcessingPoolingInterval = null)
        {
            logger.Information("Quartz starting...");

            var schedulerConfiguration = new NameValueCollection();
            schedulerConfiguration.Add("quartz.scheduler.instanceName", "Registrations");

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            IScheduler scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            scheduler.Start().GetAwaiter().GetResult();

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

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();

            ITrigger processInternalCommandsTrigger;
            if (internalProcessingPoolingInterval.HasValue)
            {
                processInternalCommandsTrigger =
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
                processInternalCommandsTrigger =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithCronSchedule("0/2 * * ? * *")
                        .Build();
            }

            scheduler.ScheduleJob(processInternalCommandsJob, processInternalCommandsTrigger).GetAwaiter().GetResult();

            logger.Information("Quartz started.");
        }
    }
}