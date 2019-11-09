# 5.   Create one REST API module

Date: 2019-07-01

Log date: 2019-11-04

## Status

Accepted

## Context

We need to expose the API of our application to the outside world. For now, we expect one client of our application - FrontEnd SPA application.

## Possible solutions
1. Create one .NET Core MVC host application which contains all endpoints. This host application will have references to all business modules and communicates with them directly:</br>

Host/API references:</br>
Administration module</br>
Meetings module</br>
Payments module</br>
User Access module</br>

2. Create one .NET Core MVC host application and multiple APIs projects per module. Each API project should have endpoints which are handled by particular business module:</br> 

Host references:</br>
Administration API references Administration module</br>
Meetings API references Meetings module</br>
Payments API references Payments module</br>
User Access API references User Access module</br>

## Decision

Solution 1.

Creating separate API projects for each module will add complexity and little value. Grouping endpoints for a particular business module in a special directory is enough. Another layer on top of the module is unnecessary.

## Consequences
- We will have only one API layer/module
- Each controller has responsibility to delegate Command/Query processing to appropriate module
- We don't need to scan other projects than host for controllers, routes and other MVC mechanisms
- API configuration is easier
- Overall complexity of API layer is lower
- Complexity of each controller is a little bit higher
- Build time will be shorter (less projects)