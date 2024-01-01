using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResult> :
        IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}