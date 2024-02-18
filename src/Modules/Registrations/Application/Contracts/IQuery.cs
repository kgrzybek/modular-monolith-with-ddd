using MediatR;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}