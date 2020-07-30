using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}