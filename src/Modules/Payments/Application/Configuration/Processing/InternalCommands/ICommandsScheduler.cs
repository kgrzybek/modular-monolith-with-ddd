using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing.InternalCommands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}