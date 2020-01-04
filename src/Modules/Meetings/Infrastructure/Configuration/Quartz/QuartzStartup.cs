using System.Collections.Specialized;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.Inbox;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.InternalCommands;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.Outbox;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        internal static void Initialize(ILogger logger)
        {
            logger.Information("Quartz starting...");

            var schedulerConfiguration = new NameValueCollection();
            schedulerConfiguration.Add("quartz.scheduler.instanceName", "Meetings");
            
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            IScheduler scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            scheduler.Start().GetAwaiter().GetResult();

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            scheduler
                .ScheduleJob(processOutboxJob, trigger)
                .GetAwaiter().GetResult();

            var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
            var processInboxTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            scheduler
                .ScheduleJob(processInboxJob, processInboxTrigger)
                .GetAwaiter().GetResult();

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();
            scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();

            logger.Information("Quartz started.");
        }
    }
}