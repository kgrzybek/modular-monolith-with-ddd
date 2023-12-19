﻿using Quartz;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Processing.Outbox;

[DisallowConcurrentExecution]
public class ProcessOutboxJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await CommandsExecutor.Execute(new ProcessOutboxCommand());
    }
}