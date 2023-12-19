using Autofac;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing
{
    /// <summary>
    /// Helper class for executing commands in the Administration module.
    /// </summary>
    internal static class CommandsExecutor
    {
        /// <summary>
        /// Executes a command without a result.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        internal static async Task Execute(ICommand command)
        {
            using var scope = AdministrationCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            await mediator.Send(command);
        }

        /// <summary>
        /// Executes a command with a result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <returns>A task representing the asynchronous operation and containing the result.</returns>
        internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            using var scope = AdministrationCompositionRoot.BeginLifetimeScope();
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(command);
        }
    }
}