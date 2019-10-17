# Modular Monolith with DDD

Full Modular Monolith .NET application with Domain-Driven Design approach.

[![Build Status](https://dev.azure.com/kgr0189/kgr/_apis/build/status/kgrzybek.modular-monolith-with-ddd?branchName=master)](https://dev.azure.com/kgr0189/kgr/_build/latest?definitionId=1&branchName=master)

## Table of contents

[1. Introduction](#1-Introduction)

&nbsp;&nbsp;[1.1 Purpose of this repository](#11-Purpose-of-this-repository)

&nbsp;&nbsp;[1.2 Out of scope](#12-Out-of-scope)

&nbsp;&nbsp;[1.3 Reason](#13-Reason)

&nbsp;&nbsp;[1.4 Disclaimer](#14-Disclaimer)

&nbsp;&nbsp;[1.5 Give a star](#15-Give-a-star)

&nbsp;&nbsp;[1.6 Share it](#16-share-it)

[2. Domain](#2-Domain)

&nbsp;&nbsp;[2.1 Description](#21-Description)

&nbsp;&nbsp;[2.2 Conceptual Model](#22-conceptual-model)

&nbsp;&nbsp;[2.3 Event Storming](#23-Event-Storming)

[3. Architecture](#3-Architecture)

&nbsp;&nbsp;[3.1 High Level View](#31-high-level-view)

&nbsp;&nbsp;[3.2 Module Level View](#32-module-level-view)

&nbsp;&nbsp;[3.3 API and Module Communication](#33-api-and-module-communication)

&nbsp;&nbsp;[3.4 Module requests processing CQRS](#34-module-requests-processing-cqrs)

&nbsp;&nbsp;[3.5 Domain Model principles and attributes](#35-domain-model-principles-and-attributes)

&nbsp;&nbsp;[3.6 Cross-cutting-concerns](#36-cross-cutting-concerns)

&nbsp;&nbsp;[3.7 Modules integration](#37-modules-integration)

&nbsp;&nbsp;[3.8 Internal processing](#38-internal-processing)

&nbsp;&nbsp;[3.9 Security](#39-security)

&nbsp;&nbsp;[3.10 Unit Tests](#310-unit-tests)

[4. Technology](#4-technology)

[5. How to run](#5-how-to-run)

[6. Contribution](#6-contribution)

[7. Roadmap](#7-roadmap)

[8. Author](#8-author)

[9. License](#9-license)

## 1. Introduction

### 1.1 Purpose of this repository

This is list of main goals of this repository:

- Showing how you can implement a **monolith** application in a **modular** way
- Presentation of the **full implementation** of an application. This is not another simple application. This is not another proof of concept (PoC). The goal is to present the implementation of an application that would be ready to run in production
- Showing the application of **best practices** and **object-oriented programming principles**
- Presentation of the use of **design patterns**. When, how and why they can be used
- Presentation of some **architectural** considerations, decisions, approaches
- Presentation of the implementation using **Domain-Driven Design** approach (tactical patterns)
- Presentation of the implementation of **Unit Tests** for Domain Model (Testable Design in mind).

### 1.2 Out of scope

This is list of subjects which are out of scope for this repository:
- Business requirements gathering and analysis
- System analysis
- Domain exploration
- Domain distillation
- Domain-Driven Design strategic patterns
- Architecture evaluation, quality attributes analysis
- Integration, system tests
- Project management
- Infrastructure
- Containerization
- Software engineering process, CI/CD
- Deployment process
- Maintenance
- Documentation

### 1.3 Reason

The reason for creating this repository is the lack of something similar. Most sample applications on GitHub have at least one issue of the following:
- it is very, very simple - few entities and use cases implemented
- it is not finished (for example there is no authentication, logging, etc..)
- it is poorly designed (in my opinion)
- it is poorly implemented (in my opinion)
- it is not well described
- assumptions and decisions are not clearly explained
- it implements "Orders" domain. Yes, everyone knows this domain, but something different is needed
- is implemented in old technology
- is not maintained

To sum up, there are some very good examples, but there are far too few of them. This repository has the task of filling this gap at some level.

### 1.4 Disclaimer

Software architecture should always be created to resolve specific **business problems**. Software architecture always supports some quality attributes and at the same time does not support others. A lot of other factors influence your software architecture - your team, opinions, preferences, experiences, technical constraints, time, budget etc.

Always functional requirements, quality attributes, technical constraints and other factors should be considered before an architectural decision is made.

Because of the above, the architecture and implementation presented in this repository is **one of the many ways** to solve some problems. Take from this repository as much as you want, use it as you like but remember to **always pick the best solution which is appropriate to the problem class you have**.

### 1.5 Give a Star

In this project on the first place I focus on its quality. Create good quality involves a lot of analysis, research and work. It takes a lot of time. If you like this project, learned something or you are using it in your applications, please give it a star :star:.  This is the best motivation for me to continue this work. Thanks!

### 1.6 Share it

As it is written above there are very few really good examples of this type of application. If you think this repository makes a difference and is worth it, please share it with your friends and on social networks. I will be extremely grateful.

## 2. Domain

### 2.1 Description

**Definition:**

> Domain - A sphere of knowledge, influence, or activity. The subject area to which the user applies a program is the domain of the software. [Domain-Driven Design Reference](http://domainlanguage.com/ddd/reference/), Eric Evans

For the purposes of this project, the meeting groups domain, based on the [Meetup](https://www.meetup.com/) system, was selected.

**Main reasons for selecting this domain:**

- It is common, a lot of people use the Meetup site to organize or attend meetings.
- There is a system for it, so everyone can check how software which supports this domain works
- It is not complex so it is easy to understand
- It is not trivial, there are some business rules and logic. It does not have only CRUD operations
- You don't need much specific domain knowledge as for other domains like financing, banking, medical
- It is not big so it is easier to implement

**Meetings**

Main business entities are ```Member```, ```Meeting Group``` and ```Meeting```. ```Member``` can create ```Meeting Group```, be part of ```Meeting Group``` or can attend the ```Meeting```. 

```Meeting Group Member``` can be an ```Organizer``` of this group or normal ```Member```.

Only ```Organizer``` of ```Meeting Group``` can create new ```Meeting```.

```Meeting``` have attendees, not attendees (```Members``` which declare they will not attend the ```Meeting```) and ```Members``` on a ```Waitlist```.

```Meeting``` can have an attendees limit. If the limit is reached, ```Members``` can only sign up to ``Waitlist``.

```Meeting Attendee``` can bring guests to a ```Meeting```. Number of guests is an attribute of ```Meeting```. Bringing guests can be not allowed.

```Meeting Attendee``` can have one of two roles : normal ```Attendee``` or ```Host```. ```Meeting``` must have at least one ```Host```. ```Host``` is special role which can edit ```Meeting``` information or change attendees list.

**Administration**

To create new ```Meeting Group```, ```Member``` needs to propose this group. ```Meeting Group Proposal``` is sent to ```Administrators```. ```Administrator``` can accept or reject ```Meeting Group Proposal```. If ```Meeting Group Proposal``` is accepted, ```Meeting Group``` is created.

**Payments**

To be able to organize ```Meetings```, the ```Meeting Group``` must be paid for. ```Meeting Group``` ```Organizer``` who is the ```Payer```, must pay some fee according to payment plan.

Additionally, Meeting organizer can set ```Event Fee```. Each ```Meeting Attendee``` is obliged to pay a fee. All guests should be paid by ```Meeting Attendee``` too.

**Users**

Each ```Administrator```,```Member``` and ```Payer``` is an ```User```. To be an ```User```, ```User Registration``` is required and confirmed.

Each ```User``` has assigned one or more ```User Role```. 

Each ```User Role``` has set of ```Permissions```. ```Permission``` defines whether ```User``` can invoke a particular action.


### 2.2 Conceptual Model

**Definition:**

> Conceptual Model - A conceptual model is a representation of a system, made of the composition of concepts that are used to help people know, understand, or simulate a subject the model represents. [Wikipedia - Conceptual model](https://en.wikipedia.org/wiki/Conceptual_model)

**Conceptual Model**

![](docs/Images/Conceptual_Model.png)

### 2.3 Event Storming

Conceptual Model focuses on structures and relationships between them. What is more important is **behavior** and **events** that occur in our domain. 

There are many ways to show behavior and events. One of them is a light technique called [Event Storming](https://www.eventstorming.com/) which is becoming more popular. Below are presented 3 main business processes using this technique : user registration, meeting group creation and meeting organization.

Note: Event Storming is a light, live workshop. Here is presented only one of the possible outputs of this workshop. Even if you are not doing Event Storming workshops, this type of presentation of process can be very valuable to you and your stakeholders.

**User Registration process**

------

![](docs/Images/User_Registration.jpg)

------

**Meeting Group creation**
![](docs/Images/Meeting_Group_Creation.jpg)

------

**Meeting organization**
![](docs/Images/Meeting_Organization.jpg)

------

## 3. Architecture

### 3.1 High Level View

![](docs/Images/Architecture_high_level.png)

Modules description:

- **API** - REST API application. Very thin, hosting ASP.NET MVC Core application. Main responsibilities are
&nbsp;&nbsp;1. Take request
&nbsp;&nbsp;1. Authenticate and Authorize request (using User Access module)
&nbsp;&nbsp;2. Delegate work to specific module sending Command or Query
&nbsp;&nbsp;3. Return response
- **User Access** - responsible for users authentication, authorization and registration
- **Meetings** - implements Meetings Bounded Context: creating meeting groups, meetings
- **Administration** - implements Administration Bounded Context: implements administrative tasks like meeting group proposal verification
- **Payments** - implements Payments Bounded Context: implements all functionalities associated with payments
- **In Memory Events Bus** - Publish/Subscribe implementation to asynchronously integrate all modules using events ([Event Driven Architecture](https://en.wikipedia.org/wiki/Event-driven_architecture)).

**Key assumptions:**

1. API doesn't have any application logic.
2. API communicates with module using a small interface to send Queries and Commands.
3. Each module has its own interface which is used by API.
4. **Modules communicate each other only asynchronously using Events Bus**. Remote Procedure Call is not allowed.
5. Each Module **has it's own data** - in separate schema or database. Shared data is not allowed.
6. Module doesn't have dependencies on other modules. Module can have only dependency on integration events assembly of other module (see [Module level view](#32-module-level-view)).
7. Each Module has its own [Composition Root](https://freecontent.manning.com/dependency-injection-in-net-2nd-edition-understanding-the-composition-root/). Which implies that each Module has its own Inversion Of Control container.
8. API as a host needs to initialize each module. Each module has an initialization method.
9. Each module is **highly encapsulated**. Only required types and members are public - the rest is internal or private.

### 3.2 Module Level View

![](docs/Images/Module_level_diagram.png)

Each Module consists of the following submodules (assemblies):

- Application - it is main submodule which is responsible for initialization, processing all requests, internal commands, integration events.
- Domain - Domain Model in Domain-Driven Design terms implementation applicable in particular [Bounded Context](https://martinfowler.com/bliki/BoundedContext.html).
- Infrastructure - implementation of infrastructural code like EF configuration and mappings.
- IntegrationEvents -  Integration Events **contracts** which are published to Events Bus. Only this assembly can be shared between other modules.

![](docs/Images/VSSolution.png)

Note: Application, Domain and Infrastructure assemblies can be merged into one assembly. Some people like horizontal layering or more decomposition, some don't. Implementing Domain Model or Infrastructure in separate assembly gives opportunity to encapsulate it using internal keyword. Sometimes Bounded Context logic is not worth it because it is too simple. As always, be pragmatic and take approach whatever you like.

### 3.3 API and Module Communication

API communicates with Module only in two places: during module initialization and request processing.

**Module initialization**

Each module has static ``Initialize`` method which is invoked in API ``Startup`` class. All configuration needed by this module should be provided as arguments to this method. During initialization all services are configured and Composition Root using Inversion Of Control Container is created.

```csharp
public static void Initialize(
    string connectionString, 
    IExecutionContextAccessor executionContextAccessor,
    ILogger logger,
    EmailsConfiguration emailsConfiguration)
{
    var moduleLogger = logger.ForContext("Module", "Meetings");

    ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger, emailsConfiguration);

    QuartzStartup.Initialize(moduleLogger);

    EventsBusStartup.Initialize(moduleLogger);
}
```

**Requests processing**

Each module has the same signature interface exposed to API. It contains 3 methods: to execute command with result, command without result and query.

```csharp
public interface IMeetingsModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

    Task ExecuteCommandAsync(ICommand command);

    Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
}
```

Note: Some people say that processing of command shouldn't return a result. This is a good approach but sometimes impractical, especially when you create a resource and want to immediately return the ID of this resource. Sometimes, boundary between Command and Query is blurry. One of the examples is ``AuthenticateCommand`` - it returns a token but it is not a query (has side effect).

### 3.4 Module requests processing CQRS

Commands and Queries processing is separated applying architectural style/pattern [Command Query Responsibility Segregation (CQRS)](https://docs.microsoft.com/pl-pl/azure/architecture/patterns/cqrs). 

![](docs/Images/CQRS.jpg)

Commands are processed using *Write Model* which is implemented using DDD tactical patterns:

```csharp
internal class CreateNewMeetingGroupCommandHandler : ICommandHandler<CreateNewMeetingGroupCommand>
{
    private readonly IMeetingGroupRepository _meetingGroupRepository;
    private readonly IMeetingGroupProposalRepository _meetingGroupProposalRepository;

    internal CreateNewMeetingGroupCommandHandler(
        IMeetingGroupRepository meetingGroupRepository, 
        IMeetingGroupProposalRepository meetingGroupProposalRepository)
    {
        _meetingGroupRepository = meetingGroupRepository;
        _meetingGroupProposalRepository = meetingGroupProposalRepository;
    }

    public async Task<Unit> Handle(CreateNewMeetingGroupCommand request, CancellationToken cancellationToken)
    {
        var meetingGroupProposal = await _meetingGroupProposalRepository.GetByIdAsync(request.MeetingGroupProposalId);

        var meetingGroup = meetingGroupProposal.CreateMeetingGroup();

        await _meetingGroupRepository.AddAsync(meetingGroup);

        return Unit.Value;
    }
}
```

Queries are processed using *Read Model* which is implemented by executing raw SQL statements on database views:

```csharp
internal class GetAllMeetingGroupsQueryHandler : IQueryHandler<GetAllMeetingGroupsQuery, List<MeetingGroupDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    internal GetAllMeetingGroupsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<List<MeetingGroupDto>> Handle(GetAllMeetingGroupsQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT " +
                           "[MeetingGroup].[Id], " +
                           "[MeetingGroup].[Name], " +
                           "[MeetingGroup].[Description], " +
                           "[MeetingGroup].[LocationCountryCode], " +
                           "[MeetingGroup].[LocationCity]" +
                           "FROM [meetings].[v_MeetingGroups] AS [MeetingGroup]";
        var meetingGroups = await connection.QueryAsync<MeetingGroupDto>(sql);

        return meetingGroups.AsList();
    }
}
```

**Key advantages:**

- Solution appropriate to the problem - reading and writing needs are usually different
- Supports SRP - one handler does one thing
- Supports ISP - each handler implements interface with exactly one method
- Commands and Queries are objects ([Parameter Object pattern](https://refactoring.com/catalog/introduceParameterObject.html)) which are easy to serialize/deserialize
- Easy way to apply Decorator pattern to handle cross-cutting concerns.
- Louse coupling - introduction of [Mediator pattern](https://en.wikipedia.org/wiki/Mediator_pattern) separates invoker of request from handler of request.

One disadvantage - introduction of mediation gives more indirection and is harder to reason about which class handles the request.

More info can be found here: [Simple CQRS implementation with raw SQL and DDD](https://www.kamilgrzybek.com/design/simple-cqrs-implementation-with-raw-sql-and-ddd/)

### 3.5 Domain Model principles and attributes

Domain Model, which is the central and most critical part in the system, should be designed with special attention. Here are some key principles and attributes which are applied to Domain Models of each module:

1. **High level of encapsulation**

All members are ``private`` by default, then ``internal``, only at the very end ``public``.

2. **High level of PI (Persistence Ignorance)**

No dependencies to infrastructure, databases, other stuff. All classes are POCO.

3. **Rich in behavior**

All business logic is located in Domain Model. No leaks to application layer or other places.

4. **Low level of primitive obsession**

Primitive attributes of Entites grouped together using ValueObjects.

5. **Business language**

All classes, methods and other members named in business language used in this Bounded Context.

6. **Testable**

Domain Model is a critical part of the system so it should be easy to test (Testable Design).

```csharp
public class MeetingGroup : Entity, IAggregateRoot
{
    public MeetingGroupId Id { get; private set; }

    private string _name;

    private string _description;

    private MeetingGroupLocation _location;

    private MemberId _creatorId;

    private List<MeetingGroupMember> _members;

    private DateTime _createDate;

    private DateTime? _paymentDateTo;

    internal static MeetingGroup CreateBasedOnProposal(
        MeetingGroupProposalId meetingGroupProposalId, 
        string name, 
        string description,
        MeetingGroupLocation location, MemberId creatorId)
    {
        return new MeetingGroup(meetingGroupProposalId, name, description, location, creatorId);
    }
    
     public Meeting CreateMeeting(
            string title,
            MeetingTerm term,
            string description,
            MeetingLocation location,
            int? attendeesLimit,
            int guestsLimit,
            Term rsvpTerm,
            MoneyValue eventFee,
            List<MemberId> hostsMembersIds,
            MemberId creatorId)
        {
            this.CheckRule(new MeetingCanBeOrganizedOnlyByPayedGroupRule(_paymentDateTo));

            this.CheckRule(new MeetingHostMustBeAMeetingGroupMemberRule(creatorId, hostsMembersIds, _members));

            return new Meeting(this.Id,
                title,
                term,
                description,
                location,
                attendeesLimit,
                guestsLimit,
                rsvpTerm,
                eventFee,
                hostsMembersIds,
                creatorId);
        }
```

### 3.6 Cross-cutting concerns

To support [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single_responsibility_principle) and [Don't Repeat Yourself](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself) principles, implementation of cross-cutting concerns is done using [Decorator Pattern](https://en.wikipedia.org/wiki/Decorator_pattern). Each Command processing is decorated by 3 decorators: logging, validation and unit of work.

![](docs/Images/Decorator.jpg)

**Logging**

Logging decorator logs execution, arguments and processing of each Command. This way each log inside processing has log context of processing command.

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

**Validation**

Validation decorator performs Command data validation. It checks rules against Command arguments. It uses FluentValidation library to do it.

```csharp
internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
{
    private readonly IList<IValidator<T>> _validators;
    private readonly ICommandHandler<T> _decorated;

    public ValidationCommandHandlerDecorator(
        IList<IValidator<T>> validators, 
        ICommandHandler<T> decorated)
    {
        this._validators = validators;
        _decorated = decorated;
    }

    public Task<Unit> Handle(T command, CancellationToken cancellationToken)
    {
        var errors = _validators
            .Select(v => v.Validate(command))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (errors.Any())
        {
            var errorBuilder = new StringBuilder();

            errorBuilder.AppendLine("Invalid command, reason: ");

            foreach (var error in errors)
            {
                errorBuilder.AppendLine(error.ErrorMessage);
            }

            throw new InvalidCommandException(errorBuilder.ToString(), null);
        }

        return _decorated.Handle(command, cancellationToken);
    }
}
```

**Unit Of Work**

Every Command processing has side effects. To not call commit on every handler, ``UnitOfWorkCommandHandlerDecorator`` is used. It additionally marks ``InternalCommand`` as processed (if it is Internal Command) and dispatches all Domain Events (as part of [Unit Of Work](https://martinfowler.com/eaaCatalog/unitOfWork.html)).

```csharp
public class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
{
    private readonly ICommandHandler<T> _decorated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MeetingsContext _meetingContext;

    public UnitOfWorkCommandHandlerDecorator(
        ICommandHandler<T> decorated, 
        IUnitOfWork unitOfWork, 
        MeetingsContext meetingContext)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _meetingContext = meetingContext;
    }

    public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
    {
        await this._decorated.Handle(command, cancellationToken);

        if (command is InternalCommandBase)
        {
            var internalCommand =
                await _meetingContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id,
                    cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = DateTime.UtcNow;
            }
        }

        await this._unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
```

### 3.7 Modules integration

Integration between modules takes place only in an **asynchronous** way using Integration Events and In Memory Events Bus as broker. In this way coupling between modules is minimal and exists only on structure of Integration Events.

**Modules don't share data** so it is not possible or desirable to create a transaction which spans more than one module. To ensure maximum reliability, [Outbox / Inbox pattern](http://www.kamilgrzybek.com/design/the-outbox-pattern/) are used. They provide accordingly *"At-Least-Once delivery"* and *"At-Least-Once processing"*.

![](docs/Images/OutboxInbox.jpg)

Outbox and Inbox is implemented using two SQL tables and background worker for each module. Background worker is implemented using Quartz.NET library.

**Saving to Outbox:**

![](docs/Images/OutboxSave.png)

**Processing Outbox:**

![](docs/Images/OutboxProcessing.png)

### 3.8 Internal processing

The main principle of this system is that you can change its state only by calling a specific Command. 

Sometimes, Command can be called not by API but by processing module itself. The main use case which uses this mechanism is data processing in eventual consistency mode, when we want to process something in a different process and transaction. This applies for example to Inbox processing, because we want to do something (calling a Command) based on Integration Event from Inbox.

This idea is taken from Alberto's Brandolini Event Storming picture called "The picture that explains “almost” everything" which shows that every side effect (domain event) is created by invoking Command on Aggregate. See [EventStorming cheat sheet](https://xebia.com/blog/eventstorming-cheat-sheet/) article for more details.

Implementation of internal processing is very similar to implementation of Outbox and Inbox. One SQL table and one background worker for processing. Each internally processing Command must inherit from ``InternalCommandBase`` class:

```csharp
internal abstract class InternalCommandBase : ICommand
{
    public Guid Id { get; }

    protected InternalCommandBase(Guid id)
    {
        this.Id = id;
    }
}
```

This is important because ``UnitOfWorkCommandHandlerDecorator`` must mark internal Command as processed during committing:

```csharp
public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
{
    await this._decorated.Handle(command, cancellationToken);

    if (command is InternalCommandBase)
    {
        var internalCommand =
            await _meetingContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id,
                cancellationToken: cancellationToken);

        if (internalCommand != null)
        {
            internalCommand.ProcessedDate = DateTime.UtcNow;
        }
    }

    await this._unitOfWork.CommitAsync(cancellationToken);

    return Unit.Value;
}
```

### 3.9 Security

**Authentication**

Authentication is implemented using JWT Token and Bearer scheme using IdentityServer. For now, only one authentication method is implemented (forms authentication by providing login and password). It requires implementation of ``IResourceOwnerPasswordValidator`` interface:

```csharp
public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly IUserAccessModule _userAccessModule;

    public ResourceOwnerPasswordValidator(IUserAccessModule userAccessModule)
    {
        _userAccessModule = userAccessModule;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var authenticationResult = await _userAccessModule.ExecuteCommandAsync(new AuthenticateCommand(context.UserName, context.Password));
        if (!authenticationResult.IsAuthenticated)
        {
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant, 
                authenticationResult.AuthenticationError);
            return;
        }
        context.Result = new GrantValidationResult(
            authenticationResult.User.Id.ToString(), 
            "forms", 
            authenticationResult.User.Claims);
    }
}
```

**Authorization**

Authorization mechanism implements [RBAC (Role Based Access Control)](https://en.wikipedia.org/wiki/Role-based_access_control) using Permissions. Permissions are more granular and much better way to secure your application than Roles. Each User has a set of Roles and each Role contains one or more Permission. With this mapping User has set of Permissions which are always checked on ``Controller`` level:

```csharp
[HttpPost]
[Route("")]
[HasPermission(MeetingsPermissions.ProposeMeetingGroup)]
public async Task<IActionResult> ProposeMeetingGroup(ProposeMeetingGroupRequest request)
{
    await _meetingsModule.ExecuteCommandAsync(
        new ProposeMeetingGroupCommand(
            request.Name, 
            request.Description, 
            request.LocationCity, 
            request.LocationCountryCode));

    return Ok();
}
```
### 3.10 Unit Tests

**Definition:**

>A unit test is an automated piece of code that invokes the unit of work being tested, and then checks some assumptions about a single end result of that unit. A unit test is almost always written using a unit testing framework. It can be written easily and runs quickly. It’s trustworthy, readable, and maintainable. It’s consistent in its results as long as production code hasn’t changed. [Art of Unit Testing 2nd Edition](https://www.manning.com/books/the-art-of-unit-testing-second-edition) Roy Osherove

**Attributes of good unit test**

- Automated
- Maitainable
- Runs very fast (in ms)
- Consistent, Deterministic (always the same result)
- Isolated from other tests
- Readable
- Can be executed by anyone
- Testing public API, not internal behavior (overspecification)
- Looks like production code
- Treated as production code

**Implementation**

Each unit test has 3 standard sections: Arrange, Act and Assert

![](docs/Images/UnitTestsGeneral.jpg)

1. Arrange

The Arrange section is responsible for preparing the Aggregate for testing the public method that we want to test. This public method is often called from the unit tests perspective as SUT (system under test). 

Creating an Aggregate ready for testing involves **calling one or more other public constructors/methods** on the Domain Model. At first it may seem that we are testing too many things at the same time, but this is not true. We need to be one hundred percent sure, that the Aggregate is in a state exactly as it will be in production. This can only be ensured when:

- **we use only public API of Domain Model**
- we don't use [InternalsVisibleToAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.internalsvisibletoattribute?view=netframework-4.8) class. This exposes Domain Model to Unit Tests library removing encapsulation so our tests and production code is treated differently and it is a very bad thing.
- we don't use [ConditionalAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.conditionalattribute?view=netframework-4.8) classes. It reduces readability and increases complexity.
- we don't create any special constructors/factory methods for tests (even with conditional compilation symbols). Special constructor/factory method only for unit tests causes duplication of business logic in the test itself and focuses on state. Additionally, this kind of approach causes the test to be very sensitive to changes and hard to maintain.
- We don't remove encapsulation from Domain Model (for example: change keywords from  internal/private to public)
- We don't make methods protected to inherit from tested class and in this way provide access to internal methods/properties. 

**Isolation of external dependencies**

There are 2 main concepts - stubs and mocks:

> A stub is a controllable replacement for an existing dependency (or collaborator) in the system. By using a stub, you can test your code without dealing with the dependency directly.

>A mock object is a fake object in the system that decides whether the unit test has passed or failed. It does so by verifying whether the object under test called the fake object as expected. There’s usually no more than one mock per test.
>[Art of Unit Testing 2nd Edition](https://www.manning.com/books/the-art-of-unit-testing-second-edition) Roy Osherove

Good advice is following: use stubs if you need to, but try to avoid mocks. Mocking causes us to test too many internal things and leads to overspecification.

2. Act

This section is very easy - we execute **exactly one** public method on aggregate (SUT).

3. Assert

In this section we check expectations. There are only 2 possible outcomes:

- Method completed and Domain Event was published (or many) or
- Business rule was broken

Simple example:
```csharp
[Test]
public void NewUserRegistration_WithUniqueLogin_IsSuccessful()
{
    // Arrange
    var usersCounter = Substitute.For<IUsersCounter>();

    // Act
    var userRegistration =
        UserRegistration.RegisterNewUser(
            "login", "password", "test@email", 
            "firstName", "lastName", usersCounter);

    // Assert
    var newUserRegisteredDomainEvent = AssertPublishedDomainEvent<NewUserRegisteredDomainEvent>(userRegistration);
    Assert.That(newUserRegisteredDomainEvent.UserRegistrationId, Is.EqualTo(userRegistration.Id));
}

[Test]
public void NewUserRegistration_WithoutUniqueLogin_BreaksUserLoginMustBeUniqueRule()
{
    // Arrange
    var usersCounter = Substitute.For<IUsersCounter>();
    usersCounter.CountUsersWithLogin("login").Returns(x => 1);

    // Assert
    AssertBrokenRule<UserLoginMustBeUniqueRule>(() =>
    {
        // Act
        UserRegistration.RegisterNewUser(
            "login", "password", "test@email",
            "firstName", "lastName", usersCounter);
    });
}
```

Advanced example:
```csharp
[Test]
public void AddAttendee_WhenMemberIsAlreadyAttendeeOfMeeting_IsNotPossible()
{
    // Arrange
    var creatorId = new MemberId(Guid.NewGuid());
    var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
    {
        CreatorId = creatorId
    });    
    var newMemberId = new MemberId(Guid.NewGuid());
    meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);
    meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 0);

    // Assert
    AssertBrokenRule<MemberCannotBeAnAttendeeOfMeetingMoreThanOnceRule>(() =>
    {
        // Act
        meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 0);
    });          
}
```

``CreateMeetingTestData`` method is an implementation of [SUT Factory](https://blog.ploeh.dk/2009/02/13/SUTFactory/) described by Mark Seemann which allows keeping common creation logic in one place:

```csharp
protected MeetingTestData CreateMeetingTestData(MeetingTestDataOptions options)
{
    var proposalMemberId = options.CreatorId ?? new MemberId(Guid.NewGuid());
    var meetingProposal = MeetingGroupProposal.ProposeNew(
        "name", "description",
        new MeetingGroupLocation("Warsaw", "PL"), proposalMemberId);

    meetingProposal.Accept();

    var meetingGroup = meetingProposal.CreateMeetingGroup();

    meetingGroup.UpdatePaymentInfo(DateTime.Now.AddDays(1));

    var meetingTerm = options.MeetingTerm ??
                      new MeetingTerm(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

    var rsvpTerm = options.RvspTerm ?? Term.NoTerm;
    var meeting = meetingGroup.CreateMeeting("title",
        meetingTerm,
        "description",
        new MeetingLocation("Name", "Address", "PostalCode", "City"),
        options.AttendeesLimit,
        options.GuestsLimit,
        rsvpTerm,
        MoneyValue.Zero,
        new List<MemberId>(),
        proposalMemberId);

    DomainEventsTestHelper.ClearAllDomainEvents(meetingGroup);

    return new MeetingTestData(meetingGroup, meeting);
}
```

## 4. Technology

List of technologies, frameworks and libraries used for implementation:

- [.NET Core 2.2](https://dotnet.microsoft.com/download) (platform)
- [MS SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express) (database)
- [Entity Framework Core 2.2](https://docs.microsoft.com/en-us/ef/core/) (ORM Write Model implementation for DDD) 
- [Autofac](https://autofac.org/) (Inversion of Control Container)
- [IdentityServer4](http://docs.identityserver.io) (Authentication and Authorization)
- [Serilog](https://serilog.net/) (structured logging)
- [Hellang.Middleware.ProblemDetails](https://github.com/khellang/Middleware/tree/master/src/ProblemDetails) (API Problem Details support)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle) (Swagger automated documentation)
- [Dapper](https://github.com/StackExchange/Dapper) (micro ORM for Read Model)
- [Newtonsoft.Json](https://www.newtonsoft.com/json) (serialization/deserialization to/from JSON)
- [Quartz.NET](https://www.quartz-scheduler.net/) (background processing)
- [FluentValidation](https://fluentvalidation.net/) (data validation)
- [MediatR](https://github.com/jbogard/MediatR) (mediator implementation)
- [Postman](https://www.getpostman.com/) (API tests)
- [NUnit](https://nunit.org/) (Testing framework)
- [NSubstitute](https://nsubstitute.github.io/) (Testing isolation framework)
- [Visual Paradigm Community Edition](https://www.visual-paradigm.com/download/community.jsp) (CASE tool for modeling and documentation)

## 5. How to run

- Download and install .NET Core 2.2 SDK
- Download and install MS SQL Server Express or other
- Create empty database and run ![InitializeDatabase.sql](src/Database/InitializeDatabase.sql) script
- Set connection string to database in appsettings.json file or using [Secrets](https://blogs.msdn.microsoft.com/mihansen/2017/09/10/managing-secrets-in-net-core-2-0-apps/)

## 6. Contribution

This project is still under analysis and development. I assume its maintenance for a long time. I would appreciate if you would like to contribute to it. Please let me know, create an Issue or Pull Request.

## 7. Roadmap

List of features/tasks/approaches to add:

| Name                     | Priority | Status | Release date |
| ------------------------ | -------- | -------- | -------- |
| Domain Model Unit Tests | High     | Completed | 2019-09-10 |
| API automated tests      | Normal   |    |    |
| FrontEnd SPA application | Normal   |    |    |
| Meeting comments feature | Low   |    |    |
| Notifications feature | Low   |    |    |
| Messages feature | Low   |    |    |
| Migration to .NET Core 3.0 | Low   |    |    |
| More advanced Payments module | Low   |    |    |

NOTE: Please don't hesitate to suggest something else or change to existing code. All proposals will be considered.

## 8. Author

Kamil Grzybek

Blog: [https://kamilgrzybek.com](https://kamilgrzybek.com)

Twitter: [https://twitter.com/kamgrzybek](https://twitter.com/kamgrzybek)

LinkedIn: [https://www.linkedin.com/in/kamilgrzybek/](https://www.linkedin.com/in/kamilgrzybek/)

GitHub: [https://github.com/kgrzybek](https://github.com/kgrzybek)

## 9. License

The project is under [MIT license](https://opensource.org/licenses/MIT).
