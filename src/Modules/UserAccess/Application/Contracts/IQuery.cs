using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}