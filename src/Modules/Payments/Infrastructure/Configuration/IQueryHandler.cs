using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration
{
    public interface IQueryHandler<in TQuery, TResult> : 
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}