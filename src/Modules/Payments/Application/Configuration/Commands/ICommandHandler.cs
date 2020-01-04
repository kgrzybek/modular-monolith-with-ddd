using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
        
    }
}