# 7. Use CQRS architectural style

Date: 2019-07-01

Log date: 2019-11-04

## Status

Accepted

## Context

Our application should handle 2 types of requests - reading and writing. </br>

For now, it looks like:</br>

- for reading, we need data model in relational form to return data in tabular/flattened way (tables, lists, dictionaries).
- for writing, we need to have a graph of objects to perform more sophisticated work like validations, business rules checks, calculations.

## Decision

We applied the CQRS architectural style/pattern for each business module. Each module will have a separate model for reading and writing. For now, it will be the simplest CQRS implementation when the read model is immediate consistent. This kind of separation is useful even in simple modules like User Access.

## Consequences
- Façade method of each module should take as parameter only Command or Query object
- We have optimized models for writes and reads (SRP principle).
- We can process Commands and Queries in different ways
- As Command or Query is an object, we can easily serialize them and save/log them.