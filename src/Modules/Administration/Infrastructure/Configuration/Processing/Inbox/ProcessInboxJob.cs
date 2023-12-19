using Quartz;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Inbox
{
    /// <summary>
    /// Represents a job that processes the inbox <see cref="IJob"/>.
    /// </summary>
    [DisallowConcurrentExecution]
    public class ProcessInboxJob : IJob
    {
        /// <summary>
        /// Executes the job by executing the ProcessInboxCommand.
        /// </summary>
        /// <param name="context">The job execution context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInboxCommand());
        }
    }
}