# 9. Use 2 layered architectural style for reads

Date: 2019-07-01

Log date: 2019-11-04

## Status

Accepted

## Context

We applied the CQRS style (see [ADR 7. Use CQRS architectural style](007-use-cqrs-architectural-style.md)), now we need to decide how to handle reading (querying) requests.

## Decision

We will use 2 layered architecture to handle queries: API layer and Application Service layer. As we applied the CQRS and created a separated read model, querying should be straightforward so 2 layers are enough. The API layer is responsible for Query creation based on HTTP request and the module Application layer is responsible for query handling.

## Consequences
- Whole query handling logic is in Application Service layer
- Application Service layer is coupled to the database and querying framework
- Solution is simple, easy to understand
- We don't abstract over database
- Performance is better (no object mapping between layers, querying database almost immediately)