# 16. Create an IoC Container per module

Date: 2019-07-15

Log date: 2019-11-09

## Status

Accepted

## Context

For each module, when we process particular Command or Query, we need to resolve a graph of objects. We need to decide how dependencies of objects will be resolved.

## Possible solutions

### 1. One IoC Container for whole application

One IoC container located in the host project.

#### Pros
- standard approach
- dependencies configured in one place
#### Cons
- couples host application with all of projects, libraries
- modules autonomy decreases
- strong coupling

### 2. IoC Container per module

Multiple IoC containers per modules.

#### Pros
- module autonomy
- loose coupling 
- host application has dependency only to Application Service Layer
#### Cons
- duplicated code
- non-standard approach

## Decision

Solution number 2 - IoC Container per module</br>

IoC Container per module supports the autonomy of the module and louse coupling so this is a more important aspect for us than duplicated code in some places.

## Consequences
- Create and maintain an IoC Container for each module
- Implementation is not standard, but still acceptable easy
- We can add dependencies to module and other modules are intact