using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Processing.InternalCommands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}