# 13. Protect business invariants using exceptions

Date: 2019-07-01

Log date: 2019-11-05

## Status

Accepted

## Context

Aggregates should check business invariants. When the invariant is broken, we should stop processing and return an error immediately to the client.

## Possible solutions

### 1. Use exceptions
#### Pros
- we can stop processing immediately (fail-fast)
-  popular approach in C# 
-  we don't need to check the result of each method (if-else statements)
-   we can catch all Business Exceptions in one place and translate them (for example in the API layer to some HTTP response code). 
#### Cons
- indirection 
- little performance impact 
- for special cases, we need to add a specific catch.
### 2. Return Result object
#### Pros 
- no indirection
- no performance impact
- signature method is more descriptive. 
### Cons 
- we need to add checks result of each method (if-else statements)
- approach is less-known in the C# world 
- it needs a library or more coding to support Results

## Decision

Solution number 1 - Use exceptions. </br>

Performance cost of throwing an exception is irrelevant, we don't want too many if/else statements in entities, more familiar with exceptions approach.

## Consequences
- We need to add special *BusinessException* class to separate business rules validation exceptions from other exceptions
- We need to create different business exceptions for each business rule
- We will have a small performance impact (throwing exceptions)
- We will have generic mechanism which catches *BusinessException*
- We will not have a lot of if/else statements in Entities/Value Objects to check method results
- Some monitoring tools logs automatically each exception. If we want to use one of this tool we should be aware of this and figure it out proper solution