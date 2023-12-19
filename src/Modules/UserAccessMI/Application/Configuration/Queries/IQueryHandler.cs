using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;

public interface IQueryHandler<in TQuery, TResult> :
    IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}