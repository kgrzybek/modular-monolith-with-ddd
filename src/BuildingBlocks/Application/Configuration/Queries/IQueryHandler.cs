using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}