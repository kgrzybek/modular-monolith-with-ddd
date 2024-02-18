namespace CompanyName.MyMeetings.Modules.Registrations.Application.Contracts
{
    public interface IRegistrationsModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}