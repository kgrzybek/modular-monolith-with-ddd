using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Processing
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
        
    }
}