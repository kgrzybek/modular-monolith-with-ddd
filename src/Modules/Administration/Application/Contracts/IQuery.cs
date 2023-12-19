using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    /// <summary>
    /// Represents a query that returns a result of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}