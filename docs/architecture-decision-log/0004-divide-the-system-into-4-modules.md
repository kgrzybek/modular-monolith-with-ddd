# 4.   Divide the system into 4 modules 

Date: 2019-07-01

Log date: 2019-11-02

## Status

Accepted

## Context

The MyMeetings domain contains 4 main subdomains: Meetings (core domain), Administration (supporting subdomain), Payments (supporting subdomain) and User Access (generic domain).

We use Modular Monolith architecture so we need to implement one application which solves all requirements from all domains listed above.

We need to modularize our system.

## Possible solutions
1. Create one "MyMeetings" module and divide it into sub-modules. This solution is simpler to implement at the beginning. We do not have to set module boundaries and think how to communicate between them. On the other hand, this causes a lack of autonomy and can lead to Big Ball Of Mud anti-pattern.
2. Create 4 modules based on Bounded Contexts which in this scenario maps 1:1 to domains. This solution is more difficult at the beginning. We need to set modules boundaries, communication strategy between modules and have more advanced infrastructure code. It is a more complex solution. On the other hand, it supports autonomy, maintainability, readability. We can develop our Domain Models in all of the Bounded Contexts independently.

## Decision

Solution 2.

We created 4 modules: Meetings, Administration, Payments, User Access. The key factor here is module autonomy and maintainability. We want to develop each module independently. This is more cleaner solution. It involves more work at the beginning but we want to invest.

## Consequences
- We can implement each module/Bounded Context independently.
- We need to set clear boundaries between modules and communication strategy between modules (and implement them)
- We need to define the API of each module
- The API/GUI layer needs to know about all of the modules
- We need to create shared libraries/classes to limit boilerplate code which will be the same in all modules
- Complexity of the whole solution will increase
- Complexity of each module will decrease
- We will have clear separation of concerns
- In addition to the application, we must divide the data
- We will have business concepts modeled in a proper way - without "godclasses" which do everything
- We can delegate development of particular module to defined team, work should be done without any conflicts on codebase