using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}