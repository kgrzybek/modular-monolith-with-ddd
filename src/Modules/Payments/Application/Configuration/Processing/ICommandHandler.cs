using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
        
    }
}