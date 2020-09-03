using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase, IRecurringCommand
    {
    }
}