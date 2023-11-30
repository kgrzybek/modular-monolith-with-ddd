using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}