# 15. Use In-Memory Events Bus

Date: 2019-07-15

Log date: 2019-11-09

## Status

Accepted

## Context

As we want to base inter-modular communication on asynchronous communication in the form of event-driven architecture, we need some "events bus" to do that.

## Possible solutions

### 1. In Memory Events Bus

In memory Publish/Subscribe implementation without any external component.

#### Pros
- very easy to implement
- no network communication needed
- performance (it depends)
- no need to learn anything
- simple solution
#### Cons
- does not support more advanced integration scenarios, everything needs to be implemented
- does not have configuration
- does not have other features (who have messaging brokers)

### 2. Message Broker

External middleware component. It could be a low-level broker (like RabbitMQ) or a high-level broker (like MassTransit, NServiceBus). 

#### Pros
- only integration with platform code needed
- more advanced integration scenarios
- richness of configuration
- a lot of features
#### Cons
- complex solution
- network communication
- new platform learning needed
- performance (it depends)

## Decision

Solution number 1 - In Memory Events Bus</br>

At that moment we don't see more advanced integration scenarios in our system than simple publish/subscribe scenario. We decided to follow the simplest scenario and if it will be necessary - move to more advanced.

## Consequences
- We need to implement Publish/Subscribe in memory
- All modules will have dependency to In Memory Events Bus to publish events/subscribe to events
- if we ever want to separate a module to another process (microservices architecture), we will need to switch to middleware