using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands
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