using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;
using FluentValidation;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing
{
    internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T>
        where T : ICommand
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly ICommandHandler<T> _decorated;

        public ValidationCommandHandlerDecorator(
            IList<IValidator<T>> validators,
            ICommandHandler<T> decorated)
        {
            this._validators = validators;
            _decorated = decorated;
        }

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