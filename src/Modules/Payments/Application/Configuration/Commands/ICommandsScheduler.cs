using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}