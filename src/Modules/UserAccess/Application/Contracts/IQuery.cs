using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}