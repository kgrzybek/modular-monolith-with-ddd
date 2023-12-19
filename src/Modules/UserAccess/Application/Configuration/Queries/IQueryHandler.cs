using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}