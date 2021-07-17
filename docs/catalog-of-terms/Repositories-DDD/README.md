# Repositories (DDD)

## Definition

*A client needs a practical means of acquiring references to preexisting domain objects. The Repository pattern is a simple conceptual framework to encapsulate those solutions[technology of data retrieval] and bring back our model focus.* [1]

*Conceptually, a Repository encapsulates the set of objects persisted in a data store and the operations performed over them, providing a more object-oriented view of the persistence layer.* [2]

*For each type of object that needs global access, create an object that can provide the **illusion of an in-memory collection** of all objects of that type. 

Set up access through a well-known global interface. 

Provide methods to add and remove objects, which will encapsulate the actual insertion or removal of data in the data store. Provide methods that select objects based on some criteria and return **fully instantiated** objects or collections of objects, thereby encapsulating the actual storage and query technology.

Provide Repositories **only for Aggregate roots that actually need direct access**. Keep the client focused on the model, delegating all object storage and access to the Repositories.* [1]

 *The implementation of a repository can be closely liked to the infrastructure, but that the repository interface will be pure domain model.* [4] (see [Dependency Inversion](../Dependency-Inversion/))


## Frequent questions
> What other queries can the Repository provide?

*They can also return summary information, such as a count of how many instances meet some criteria. They can even return summary calculations, such as the total across all matching objects of some numerical attribute.* [1]

> Should Repository actually commit data to a data store?

*Although the Repository will insert into and delete from the database, it will ordinarily not commit anything. It is tempting to commit after saving, for example, but the client presumably has the context to correctly initiate and commit units of work. Transaction management will be simpler if the Repository keeps its hands off.**

> Does generic repository fit the pattern?

*The Repository represents the domain’s contract with a data store. This is extremely important as one can tell every possible way that the domain interacts with the data store by looking at the contract. Allowing query objects to be passed into the [generic]repository widens the contract to a point of uselessness.* [3]

> Do I have to use the Repository to retrieve data for queries from a client of my application?

See [CQRS pattern](../CQRS/).

## Example

### Model

![](http://www.plantuml.com/plantuml/png/ZOz1gW8n343tFKKlq2jyPlWkd8KRyGAXpOp1DYrfkXZKkpk2ek8i38H0o7j9kgeWsb8qfe0_mOHsanCGsxEnIn0hoWWUxR13LE9fZoLNqYopkRwWlfH87fJoa_GHQhLz20iGvqD-uFyv9MIz5-2mNJAYl9i67WMgIlFQ13zagnVyji4weUcexc_jZw-ETQsuo-giwP13I_46)

### Code

```csharp

public interface IMeetingCommentRepository
{
    Task AddAsync(MeetingComment meetingComment);

    Task<MeetingComment> GetByIdAsync(MeetingCommentId meetingCommentId);
}

public class MeetingCommentRepository : IMeetingCommentRepository
{
    private readonly MeetingsContext _meetingsContext;
    public MeetingCommentRepository(MeetingsContext meetingsContext)
    {
        _meetingsContext = meetingsContext;
    }
    public async Task AddAsync(MeetingComment meetingComment)
    {
        await _meetingsContext.MeetingComments.AddAsync(meetingComment);
    }
    public async Task<MeetingComment> GetByIdAsync(MeetingCommentId meetingCommentId)
    {
        return await _meetingsContext.MeetingComments.FindAsync(meetingCommentId);
    }
}

```

### Description

```IMeetingCommentRepository``` is part of Domain model and provide methods two add and get Meeting aggregate. ```MeetingCommentRepository``` implements ```IMeetingCommentRepository``` and lays in the Infrastructure layer. The implementation uses ORM (Entity Framework) to interact with data store. One of the reason you won't see many methods in repositories of the project is using CQRS.

## Sources
1. [Domain-Driven Design: Tackling Complexity in the Heart of Software, Eric Evans](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215)
1. [Patterns of Enterprise Application Architecture Catalog](https://martinfowler.com/eaaCatalog/repository.html)
1. [DDD: The Generic Repository, Greg Young](http://codebetter.com/gregyoung/2009/01/16/ddd-the-generic-repository/)
1. [DDD Quickly]()