using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}