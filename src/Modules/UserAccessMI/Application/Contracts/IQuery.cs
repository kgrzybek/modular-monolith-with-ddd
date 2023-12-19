using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

public interface IQuery<out TResult> : IRequest<TResult>
{
}