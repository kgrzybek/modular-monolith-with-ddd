# 10. Use Clean Architecture for writes

Date: 2019-07-01

Log date: 2019-11-05

## Status

Accepted

## Context

We applied the CQRS style (see [ADR #7](0007-use-cqrs-architectural-style.md)), now we need to decide how to handle writing operations (Commands).

## Decision

We will use **Clean Architecture** to handle commands with 4 layers: **API layer**, **Application Service layer**, **Infrastructure layer** and **Domain layer**. </br>
We need to add Domain layer because domain logic will be complex and we want to isolate this logic from other stuff like infrastructure or API. Isolation of domain logic supports testing, maintainability and readability.

## Consequences
- Complexity of the whole solution is higher - we need to add two more layers - domain and infrastructure
- Complexity of implementation of business logic will be smaller - we can focus only on business concerns on this layer
- Complexity of implementation of infrastructure will be smaller - we can focus only on infrastructure concerns on this layer
- Business logic will be testable (no references to other layers)
- Business logic will be independent of persistence (to some level)
- In the Domain layer, we will have the same level of abstraction (close to business)
- Application layer will have louse coupling to the Domain layer and Infrastructure layer (depend  on abstractions only)
- We use one of the most popular application architecture - developers are familiar with it
