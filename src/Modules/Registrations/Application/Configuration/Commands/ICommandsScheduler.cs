using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}