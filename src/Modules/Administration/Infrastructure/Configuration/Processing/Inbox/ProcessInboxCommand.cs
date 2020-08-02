using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Inbox
{
    public class ProcessInboxCommand : CommandBase<Unit>, IRecurringCommand
    {
    }
}