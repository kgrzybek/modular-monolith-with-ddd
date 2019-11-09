# 8. Allow return result after command processing

Date: 2019-07-01

Log date: 2019-11-04

## Status

Accepted

## Context

The theory of the CQRS and the CQS principle says that we should not return any information as the result of Command processing. The result should be always "void". However, sometimes we need to return some data immediately as part of the same request.

## Decision

We decided to allow in some cases return results after command processing. Especially, when we create something and we need to return the ID of created object or don't know if request is Command or Query (like Authentication).

## Consequences
- We will have two definitions of Commands and CommandHandlers - with and without result
- It will add some complexity to processing commands (like implementation of decorators)
- We can immediately return the ID of created object/resource. We don't need a second call (query) to retrieve this ID.
- We should be careful to not overuse this approach (sticking as much as possible to the CQRS)