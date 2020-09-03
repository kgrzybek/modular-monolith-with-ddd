using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}