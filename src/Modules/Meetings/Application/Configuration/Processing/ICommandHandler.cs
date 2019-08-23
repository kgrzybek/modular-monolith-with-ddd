using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Processing
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
        
    }
}