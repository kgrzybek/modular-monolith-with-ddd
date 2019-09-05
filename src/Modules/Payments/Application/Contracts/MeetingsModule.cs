using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Contracts
{
    public class PaymentsModule : IPaymentsModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            return await CommandsExecutor.Execute(query);
        }
    }
}