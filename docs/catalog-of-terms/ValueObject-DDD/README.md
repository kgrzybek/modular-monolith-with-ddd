# ValueObject (DDD)

## Definition

*When you care only about the attributes of an element of the model, classify it as a VALUE OBJECT. Make it express the meaning of the attributes it conveys and give it related functionality. Treat the VALUE OBJECT as immutable. Don't give it any identity and avoid the design complexities necessary to maintain ENTITIES.*

Source: [Domain-Driven Design: Tackling Complexity in the Heart of Software, Eric Evans](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215)

## Example

### Model

![](http://www.plantuml.com/plantuml/png/7OwnSiCm34FtVaNx0JBtJXwyT-jEYupjc19z51bVylMniW27Ty0TnkPe7aM-VhQQ9OZ3v7jrFzelWE4vB9klCKTZorgTgmzP2-oBlPupxr2KGj1IqQfoLTFPXOYWO7Cs8CqDCZgABablwMAbmJzAyDzyv-nfcYPuz9pq0_fwEFgdaIjT_WO0)

### Code

```csharp

public class MoneyValue : ValueObject
{
    public decimal Value { get; }

    public string Currency { get; }

    private MoneyValue(decimal value, string currency)
    {
        this.Value = value;
        this.Currency = currency;
    }

    public static MoneyValue Of(decimal value, string currency)
    {
        CheckRule(new ValueOfMoneyMustNotBeNegativeRule(value));

        return new MoneyValue(value, currency);
    }

    public static bool operator >(decimal left, MoneyValue right) => left > right.Value;

    public static bool operator <(decimal left, MoneyValue right) => left < right.Value;

    public static bool operator >=(decimal left, MoneyValue right) => left >= right.Value;

    public static bool operator <=(decimal left, MoneyValue right) => left <= right.Value;

    public static bool operator >(MoneyValue left, decimal right) => left.Value > right;

    public static bool operator <(MoneyValue left, decimal right) => left.Value < right;

    public static bool operator >=(MoneyValue left, decimal right) => left.Value >= right;

    public static bool operator <=(MoneyValue left, decimal right) => left.Value <= right;
}

```

### Description

A *Money Value* class represents concept of money. In our Domain, we don't want to follow money in time (it does not have a life cycle). It does not have an identity either. Whole object is **immutable** (`Value` and `Currency` defined as readonly). The comparison is done by comparing attribute values, not identifiers (see `ValueObject` abstract class).