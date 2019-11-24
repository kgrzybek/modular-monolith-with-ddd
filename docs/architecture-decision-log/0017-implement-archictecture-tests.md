# 17. Implement Architecture Tests

Date: 2019-11-16

## Status

Accepted

## Context

In some cases it is not possible to enforce the application architecture, design or established conventions using compiler (compile-time). For this reason, code implementations can diverge from the original design and architecture. We want to minimize this behavior, not only by code review.

## Decision

We decided to implement Unit Tests for our architecture. </br>
We will implement tests for each module separately and one tests library for general architecture. We will use _NetArchTest_ library which was created exactly for this purpose.

## Consequences
- We will have quick feedback about breaking the design rules
- Unit tests for architecture are documenting our architecture to some level
- We will have dependency to external library
- We need to implement some _"reflection-based"_ code to check some rules, because library does not provide everything what we need
- This kind of tests are a bit slower than normal unit tests (because of reflection)
- More tests to maintain