using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}