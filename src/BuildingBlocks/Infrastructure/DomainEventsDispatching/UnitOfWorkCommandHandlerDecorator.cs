using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Decorator for command handlers that provides unit of work functionality.
    /// </summary>
    /// <typeparam name="T">The type of the command being handled.</typeparam>
    public class UnitOfWorkCommandHandlerDecorator<T> : IRequestHandler<T>
        where T : IRequest
    {
        private readonly IRequestHandler<T> _decorated;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkCommandHandlerDecorator{T}"/> class.
        /// </summary>
        /// <param name="decorated">The decorated command handler.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public UnitOfWorkCommandHandlerDecorator(
            IRequestHandler<T> decorated,
            IUnitOfWork unitOfWork)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the command asynchronously, committing the unit of work after handling it.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            await this._decorated.Handle(command, cancellationToken);

            await this._unitOfWork.CommitAsync(cancellationToken);
        }
    }
}