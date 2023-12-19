using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}