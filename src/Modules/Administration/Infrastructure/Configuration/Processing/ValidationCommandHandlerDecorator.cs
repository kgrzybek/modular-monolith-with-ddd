using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using FluentValidation;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing
{
    /// <summary>
    /// Decorator class that adds validation functionality to a command handler.
    /// </summary>
    /// <typeparam name="T">The type of command to handle.</typeparam>
    internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T>
        where T : ICommand
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly ICommandHandler<T> _decorated;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationCommandHandlerDecorator{T}"/> class.
        /// </summary>
        /// <param name="validators">The validators to use.</param>
        /// <param name="decorated">The decorated command handler.</param>
        public ValidationCommandHandlerDecorator(
            IList<IValidator<T>> validators,
            ICommandHandler<T> decorated)
        {
            this._validators = validators;
            _decorated = decorated;
        }

        /// <summary>
        /// Handles a command by performing validation before invoking the decorated command handler.
        /// </summary>
        /// <typeparam name="T">The type of command being handled.</typeparam>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());
            }

            await _decorated.Handle(command, cancellationToken);
        }
    }
}