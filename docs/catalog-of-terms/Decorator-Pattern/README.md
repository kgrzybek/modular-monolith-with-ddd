# Decorator Pattern

## Definition

*In object-oriented programming, the decorator pattern is a design pattern that allows behavior to be added to an individual object, dynamically, without affecting the behavior of other objects from the same class. The decorator pattern is often useful for adhering to the [Single Responsibility Principle](../Single-Responsibility-Principle/), as it allows functionality to be divided between classes with unique areas of concern.*

Source: [Wikipedia](https://en.wikipedia.org/wiki/Decorator_pattern)


## Example

### Model

![](http://www.plantuml.com/plantuml/png/bPFDhjCm48NtVehPqGitVO3eghfLMO044eA-W1jF4Wjx9h8dL8YVPyVDJqL9g66HHMRE-Sx46Jz7qK5wxrJbT8pm7b4iDV70986Tmm3V5BpQ6pDrzY981d7paCeVqVCNN7P-g0ctz1tOUqrcwgy2Pibr96Ery3Z8fwGO0om9XbfN26ydmvlqE0nFbk0ubNQ3QMnivk8Z73HLw9mMotJapqW3yMTcved_EtAfpMSilpiRSq-UINh7JPDCj-pNM76udEdJyVQ4Lc5G9CZc0IvKOa5mM0jmdM6NPUehW0yOQWu-WXlbZt3g1GnZf1UI-bMhgK5e-GmZ0hZ3HC2ea0nS4fLghK50tybNyEXF6A9IAxjQ63vJiRlkJ0cMXl3_4wkvD6l-lXGbnFGQbuxbFrkQL7RNYhwxdzuEmgZck3niLEPu-T6smJQjRB_l_ho09LZVEVH84Y4_sB-ZrFs5Wss6aFE_N0UOKO1HFIEPthjV)

### Code

```csharp

internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
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
    public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
    {
        if (command is IRecurringCommand)
        {
            return await _decorated.Handle(command, cancellationToken);
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

                var result = await _decorated.Handle(command, cancellationToken);

                this._logger.Information("Command {Command} processed successful", command.GetType().Name);

                return result;
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

```
### Description

The Logging decorator logs execution, arguments and processing of each Command. This way each log inside a processor has the log context of the processing command.

A unique trade of a decorator is that it does both:
* It implements  `ICommandHandler<T>` (known also as the *component*).
* It accepts an <u>implementation</u> of `ICommandHandler<T>` (known also as the *concrete component*). Usually that is done via [Dependency Injection](../Dependency-Injection/).

The decorator builds on top of existing functionality provided by the injected `ICommandHandler<T>`, but it does not change the behavior of it.

---

Decorator should not be confused with [Strategy](../Strategy-Pattern/)!!!
*A decorator lets you change the skin of an object, while Strategy lets you change the guts.*