namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

public interface IUserAccessModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

    Task ExecuteCommandAsync(ICommand command);

    Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
}