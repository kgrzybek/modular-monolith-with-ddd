# 11. Create rich Domain Models

Date: 2019-07-01

Log date: 2019-11-05

## Status

Accepted

## Context

We need to create Domain Models for all of the modules. Each Domain Model should represent a solution that solves a particular set of Domain problems (implements business logic).

## Possible solutions
1. Create Anemic Domain Model (Data Model) and implement *Transaction Script* pattern together with *Active Record* pattern
2. Put all business logic to database in stored procedures
3. Create a Rich Domain Model

## Decision

Solution number 3 - Rich Domain Model</br>

1 - no, because the procedural style of coding will not be enough. We want to focus on behavior, not on the data.</br>
2 - no, keeping business logic in the database is not a good idea in that case, object-oriented programming is better than T-SQL to model our domain and we don't have performance architectural drivers to resign from OOD.</br>

We expect complex business logic with different rules, calculations and processing so we want to get as much as possible from Object-Oriented Design principles like abstraction, encapsulation, polymorphism. We want to mutate the state of our objects only through methods (abstraction) to encapsulate all logic and hide implementation details from the client (the Application Service Layer and Unit Tests).</br>

## Consequences
- All objects should be encapsulated (private by default principle) 
- Encapsulation of objects implies more work in infrastructure (mapping to private fields, collections is harder)
- Encapsulation of objects decreases to some level testability of these objects (Object-Oriented Design vs Testable Design)
- All public methods of domain objects create Domain Model API
- Implementation details of business logic are hidden
- Clients of Domain Model are easier to implement
- Better object-oriented programming skills are required to implement Rich Domain Model
- Is easier to protect business rules/invariants using Rich Domain Model