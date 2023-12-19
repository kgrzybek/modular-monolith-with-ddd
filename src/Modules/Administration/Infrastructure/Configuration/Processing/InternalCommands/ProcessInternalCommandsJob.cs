using Quartz;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands
{
    /// <summary>
    /// Represents a job that processes internal commands <see cref="IJob"/>.
    /// </summary>
    [DisallowConcurrentExecution]
    public class ProcessInternalCommandsJob : IJob
    {
        /// <summary>
        /// Executes the job by executing the <see cref="ProcessInternalCommandsCommand"/>.
        /// </summary>
        /// <param name="context">The job execution context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInternalCommandsCommand());
        }
    }
}