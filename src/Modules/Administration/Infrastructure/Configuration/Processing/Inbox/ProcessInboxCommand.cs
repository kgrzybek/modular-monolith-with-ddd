using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Inbox
{
    /// <summary>
    /// Represents a command for processing in the inbox.
    /// </summary>
    public class ProcessInboxCommand : CommandBase, IRecurringCommand
    {
    }
}