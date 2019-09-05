using System.Threading.Tasks;
using Autofac;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing
{
    internal static class CommandsExecutor
    {
        internal static async Task Execute(IRequest command)
        {
            using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        internal static async Task<TResult> Execute<TResult>(IRequest<TResult> command)
        {
            using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }
    }
}