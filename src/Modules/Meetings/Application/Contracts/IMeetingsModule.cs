namespace CompanyName.MyMeetings.Modules.Meetings.Application.Contracts
{
    public interface IMeetingsModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}