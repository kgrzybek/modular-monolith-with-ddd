using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing
{
    internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T>
        where T : ICommand
    {
        private readonly ILogger _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ICommandHandler<T> _decorated;

        public LoggingCommandHandlerDecorator(
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            ICommandHandler<T> decorated)
        {
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;
            _decorated = decorated;
        }

        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            if (command is IRecurringCommand)
            {
                 await _decorated.Handle(command, cancellationToken);

                 return;
            }

            using (
                LogContext.Push(
                    new RequestLogEnricher(_executionContextAccessor),
                    new CommandLogEnricher(command)))
            {
                try
                {
                    this._logger.Information(
                        "Executing command {Command}",
                        command.GetType().Name);

                    await _decorated.Handle(command, cancellationToken);

                    this._logger.Information("Command {Command} processed successful", command.GetType().Name);
                }
                catch (Exception exception)
                {
                    this._logger.Error(exception, "Command {Command} processing failed", command.GetType().Name);
                    throw;
                }
            }
        }

        private class CommandLogEnricher : ILogEventEnricher
        {
            private readonly ICommand _command;

            public CommandLogEnricher(ICommand command)
            {
                _command = command;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Command:{_command.Id.ToString()}")));
            }
        }

        private class RequestLogEnricher : ILogEventEnricher
        {
            private readonly IExecutionContextAccessor _executionContextAccessor;

            public RequestLogEnricher(IExecutionContextAccessor executionContextAccessor)
            {
                _executionContextAccessor = executionContextAccessor;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                if (_executionContextAccessor.IsAvailable)
                {
                    logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId", new ScalarValue(_executionContextAccessor.CorrelationId)));
                }
            }
        }
    }
}