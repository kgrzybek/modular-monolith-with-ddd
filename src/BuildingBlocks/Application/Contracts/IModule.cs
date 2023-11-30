using System.Threading.Tasks;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Contracts
{
    public interface IModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}