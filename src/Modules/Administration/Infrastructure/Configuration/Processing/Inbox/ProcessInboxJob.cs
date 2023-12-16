using Quartz;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Inbox
{
    [DisallowConcurrentExecution]
    public class ProcessInboxJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInboxCommand());
        }
    }
}