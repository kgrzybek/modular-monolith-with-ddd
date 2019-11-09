# 14. Event-driven communication between modules

Date: 2019-07-15

Log date: 2019-11-09

## Status

Accepted

## Context

Each module should be autonomous. However, communication between them must take place. We have to decide what will be the preferred way of communication and integration between modules.

## Possible solutions

### 1. Direct method call (synchronous)

Each Module will expose a set of methods (interface, module API) which can be called by other modules directly.

#### Pros
- easier implementation
- no indirection
- more natural in the monolith architecture
- supports immediate consistency
#### Cons
- less autonomy
- strong coupling between modules
- direct method call is blocking
- module has a dependency on another module

### 2. Event-driven (asynchronous)

Each module will publish a specific set of events. Other modules can subscribe to specific events. It is the implementation of _Publish/Subscribe_ pattern.

#### Pros
- more autonomy
- coupling is only to middleware/broker of events
- no blocking communication
- stronger modules boundaries
- module does not have a dependency on another module
#### Cons
- indirection
- more complex solution
- middleware/broker is needed
- does not support immediate consistency

## Decision

Solution number 2 - Event-driven (asynchronous)</br>

We want to achieve the maximum level of autonomy and loose coupling between modules. Moreover, we don't want dependencies between modules. We allow direct calls in the future, but this should be an exception, not a rule.

## Consequences
- We need to implement the Publish/Subscribe pattern
- Solution will be more complex
- Modules will have more autonomy
- Modules will have coupling to broker/middleware
- During modules integration, eventual consistency will occur (asynchronous communication)
- Events become Published Language of our Bounded Contexts (modules)
- Events structure should be stable as much as possible