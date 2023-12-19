using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox
{
    /// <summary>
    /// Represents a command for processing the outbox message.
    /// </summary>
    public class ProcessOutboxCommand : CommandBase, IRecurringCommand
    {
    }
}