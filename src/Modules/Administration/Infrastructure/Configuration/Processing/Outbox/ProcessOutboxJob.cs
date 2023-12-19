using Quartz;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox
{
    /// <summary>
    /// Represents a job that processes the outbox pattern.
    /// </summary>
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        /// <summary>
        /// Executes the job by executing the <see cref="ProcessOutboxCommand"/>.
        /// </summary>
        /// <param name="context">The job execution context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessOutboxCommand());
        }
    }
}