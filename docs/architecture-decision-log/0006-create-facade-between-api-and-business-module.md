# 6. Create façade between API and business module

Date: 2019-07-01

Log date: 2019-11-04

## Status

Accepted

## Context

Our API layer should communicate with business modules to fulfill client requests. To support the maximum level of autonomy, each module should expose a minimal set of operations (the module API/contract/interface).

## Decision

Each module will provide implementation for one interface with 3 methods:</br>

```csharp
Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

Task ExecuteCommandAsync(ICommand command);

Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
```

This interface will act as a façade (Façade pattern) between API and module. Only Commands, Queries and returned objects (which are part of this interface) should be visible to the API. Everything else should be hidden behind the façade (module encapsulation).

## Consequences
- API can communicate with the module only by façade (the interface).
- Implementation of API is simpler
- We can change module implementation and API does not require change if the interface is not changed
- We need to focus on module encapsulation, sometimes it involves additional work (like instantiation using internal constructors)