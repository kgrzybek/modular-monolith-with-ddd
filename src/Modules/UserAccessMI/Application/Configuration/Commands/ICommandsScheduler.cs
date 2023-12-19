using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;

public interface ICommandsScheduler
{
    Task EnqueueAsync(ICommand command);

    Task EnqueueAsync<T>(ICommand<T> command);
}