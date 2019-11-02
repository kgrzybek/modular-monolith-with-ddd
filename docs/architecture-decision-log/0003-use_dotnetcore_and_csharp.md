# 3.  Use .NET Core and C# language

Date: 2019-07-01

Log date: 2019-10-28

## Status

Accepted

## Context

As it is monolith, only one language (or platform) must be selected for implementation.

## Decision

I decided to use:

- .NET Core platform - it is new generation multi-platform, fully supported by Microsoft and open-source community, optimized and designed to replace old .NET Framework
- C# language - most popuplar language in .NET ecosystem, I have 12 years commercial experience
- F# will not be used, I don't have commercial experience with it

## Consequences

- Whole application will be implemented in C# object-oriented language in .NET Core framework
- .NET Core applications can be executed on Windows, MacOS, Linux