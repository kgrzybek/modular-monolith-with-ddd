# Domain Event

## Definition

*An event is something that has happened in the past. A **domain event** is, something that happened in the domain that you want other parts of the same domain (in-process) to be aware of. The notified parts usually react somehow to the events.*

Source: [Domain events: design and implementation](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation)

## Example

### Model

![](http://www.plantuml.com/plantuml/png/ZL31Ii0m3BttAtgN_S3mC9mUTfemy1xR28Ks7Kag3lNVZMC7BHwyb9UyrvV7cqI1jPNiGWOHlxLd2PnsJPKUuIX8EZE2Ohol1H8zlDh6lpj_yuToYROtZ6oeKo2d6kSQqOYvDb8-hcbJq2O6dY2tasxCIE5mdrUe7wVlGD0bKkGN2EYNFjLvU0tXsoAkP1Rkb-RsOnXwlz6dicSiDehhkFF3_rePFRufKXGtsMkLVW40)

### Code

```csharp

public class SubscriptionPaymentCreatedDomainEvent : DomainEventBase
{
    public SubscriptionPaymentCreatedDomainEvent(
        Guid subscriptionPaymentId,
        Guid payerId,
        string subscriptionPeriodCode,
        string countryCode,
        string status,
        decimal value,
        string currency)
    {
        SubscriptionPaymentId = subscriptionPaymentId;
        PayerId = payerId;
        SubscriptionPeriodCode = subscriptionPeriodCode;
        CountryCode = countryCode;
        Status = status;
        Value = value;
        Currency = currency;
    }

    public Guid SubscriptionPaymentId { get; }
    public Guid PayerId { get; }
    public string SubscriptionPeriodCode { get; }
    public string CountryCode { get; }
    public string Status { get; }
    public decimal Value { get; }
    public string Currency { get; }
}

public class DomainEventBase : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }

    public DomainEventBase()
    {
        this.Id = Guid.NewGuid();
        this.OccurredOn = DateTime.UtcNow;
    }
}

```
### Description

A `SubscriptionPaymentCreatedDomainEvent` gets fired within the `SubscriptionPayment` aggregate root. This happens whenever a `Member` who is also a `Payer`, issues a command to buy a `Subscription`. 

All properties are `get` only, because an event is something that has happened in the past, and you can not change the past.

Event details:
* `SubscriptionPaymentId` - Auto generated unique Id for the subscription payment.
* `PayerId` - The Id of the payer wanting to buy a subscription.
* `SubscriptionPeriodCode` - The period of validity for this subscription:
  * 1 Month
  * 6 Months
  * Custom
* `CountryCode` - The code of the country that the payer is issuing from.
* `Status` - Automatically set to *WaitingForPayment*.
* `Value` - The amount to be paid for the chosen subscription period.
* `Currecy` - The currency of choice of the payer.

---
Business domain events extend `DomainEventBase` which in-turn implements the `IDomainEvent` interface.

Base event details:
* `Id` - Auto generated unique Id for the event itself.
* `OccurredOn` - The point-in-time when the event happened. 