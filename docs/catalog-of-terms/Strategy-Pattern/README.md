# Strategy Pattern

## Definition

*The strategy pattern (also known as the policy pattern) is a behavioral software design pattern that enables selecting an algorithm at runtime. Instead of implementing a single algorithm directly, code receives run-time instructions as to which in a family of algorithms to use.*

Source: [Wikipedia](https://en.wikipedia.org/wiki/Strategy_pattern)

## Example

### Model

![](http://www.plantuml.com/plantuml/png/jLDDQnin4BtFhnZIYzFQsxin9lM6f8KU38PUYooDNL5z66cMPd7wtwlrHifs3Oqfs63GpBpvUBFpxYABm8qrS13ofzWJtZoIew3b3RwxF_tm2DA86B4scXmd4sSelMDwOlWDETWx-cZa89ZsBU07ZCIR5tEI_RTTGFd9RPUlKsBO2KcOSNZiulH4ic7gGQM93CIKWPykHgxEaGbREA-QTjDiempwmDgxS-uZGEsj5KvzJdz3eQp4aUoYdRKEMj9N7Vb1IFQXhUf0Wgcu9w_mmTWbt9VyVaYsTllDO9zzdObcid6A1J1Ox2FngKvgqJWERUqLJJ4EnbzJq5vDKNP9QRZHT_Yo_hig7l-tR25shmD9_YPCGm_1syBpgfskKJoUqaXTbKhgzqChGh87Rj6ItLA802y2d2d_oysUbrbpaBNtVZOh6e8on-8vkS-KyqPy1U0y4nhQCVfTRZMVAu-0jJ0cucAxp8AkYh0M7xTB8AUmImU0Vmkdfx9ylNl8hvxD-19Xw2ZJltLTbsTVc73v5OpMM63pURwCuJh7Ug_A-LHLDLhjNNerrlm1)

### Code

```csharp

internal class BuySubscriptionCommandHandler : ICommandHandler<BuySubscriptionCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        private readonly IPayerContext _payerContext;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal BuySubscriptionCommandHandler(
            IAggregateStore aggregateStore,
            IPayerContext payerContext,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _aggregateStore = aggregateStore;
            _payerContext = payerContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Guid> Handle(BuySubscriptionCommand command, CancellationToken cancellationToken)
        {
            var priceList = await PriceListFactory.CreatePriceList(_sqlConnectionFactory.GetOpenConnection());

            var subscription = SubscriptionPayment.Buy(
                _payerContext.PayerId,
                SubscriptionPeriod.Of(command.SubscriptionTypeCode),
                command.CountryCode,
                MoneyValue.Of(command.Value, command.Currency),
                priceList);

            _aggregateStore.AppendChanges(subscription);

            return subscription.Id;
        }
    }

public static class PriceListFactory
    {
        public static async Task<PriceList> CreatePriceList(IDbConnection connection)
        {
            var priceListItemList = await GetPriceListItems(connection);

            var priceListItems = priceListItemList
                .Select(x =>
                    new PriceListItemData(
                        x.CountryCode,
                        SubscriptionPeriod.Of(x.SubscriptionPeriodCode),
                        MoneyValue.Of(x.MoneyValue, x.MoneyCurrency),
                        PriceListItemCategory.Of(x.CategoryCode)))
                .ToList();

            // This is place for selecting pricing strategy based on provided data and the system state.
            IPricingStrategy pricingStrategy = new DirectValueFromPriceListPricingStrategy(priceListItems);

            return PriceList.Create(
                priceListItems,
                pricingStrategy);
        }

        public static async Task<List<PriceListItemDto>> GetPriceListItems(IDbConnection connection)
        {
            var priceListItems = await connection.QueryAsync<PriceListItemDto>("SELECT " +
$"[PriceListItem].[CountryCode] AS [{nameof(PriceListItemDto.CountryCode)}], " +
$"[PriceListItem].[SubscriptionPeriodCode] AS [{nameof(PriceListItemDto.SubscriptionPeriodCode)}], " +
$"[PriceListItem].[MoneyValue] AS [{nameof(PriceListItemDto.MoneyValue)}], " +
$"[PriceListItem].[MoneyCurrency] AS [{nameof(PriceListItemDto.MoneyCurrency)}], " +
$"[PriceListItem].[CategoryCode] AS [{nameof(PriceListItemDto.CategoryCode)}] " +
"FROM [payments].[PriceListItems] AS [PriceListItem] " +
"WHERE [PriceListItem].[IsActive] = 1");

            var priceListItemList = priceListItems.AsList();
            return priceListItemList;
        }
    }

public class PriceList : ValueObject
    {
        private readonly List<PriceListItemData> _items;

        private readonly IPricingStrategy _pricingStrategy;

        private PriceList(
            List<PriceListItemData> items,
            IPricingStrategy pricingStrategy)
        {
            _items = items;
            _pricingStrategy = pricingStrategy;
        }

        public static PriceList Create(
            List<PriceListItemData> items,
            IPricingStrategy pricingStrategy)
        {
            return new PriceList(items, pricingStrategy);
        }

        public MoneyValue GetPrice(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            PriceListItemCategory category)
        {
            CheckRule(new PriceForSubscriptionMustBeDefinedRule(countryCode, subscriptionPeriod, _items, category));

            return _pricingStrategy.GetPrice(countryCode, subscriptionPeriod, category);
        }
    }

public interface IPricingStrategy
    {
        MoneyValue GetPrice(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            PriceListItemCategory category);
    }

public class DiscountedValueFromPriceListPricingStrategy : IPricingStrategy
    {
        private readonly List<PriceListItemData> _items;

        private readonly MoneyValue _discountValue;

        public DiscountedValueFromPriceListPricingStrategy(
            List<PriceListItemData> items,
            MoneyValue discountValue)
        {
            _items = items;
            _discountValue = discountValue;
        }

        public MoneyValue GetPrice(string countryCode, SubscriptionPeriod subscriptionPeriod, PriceListItemCategory category)
        {
            var priceListItem = _items.Single(x =>
                x.CountryCode == countryCode && x.SubscriptionPeriod == subscriptionPeriod &&
                x.Category == category);

            return priceListItem.Value - _discountValue;
        }
    }

 public class DirectValuePricingStrategy : IPricingStrategy
    {
        private readonly MoneyValue _directValue;

        public DirectValuePricingStrategy(MoneyValue directValue)
        {
            _directValue = directValue;
        }

        public MoneyValue GetPrice(string countryCode, SubscriptionPeriod subscriptionPeriod, PriceListItemCategory category)
        {
            return _directValue;
        }
    }

public class DirectValueFromPriceListPricingStrategy : IPricingStrategy
    {
        private readonly List<PriceListItemData> _items;

        public DirectValueFromPriceListPricingStrategy(List<PriceListItemData> items)
        {
            _items = items;
        }

        public MoneyValue GetPrice(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            PriceListItemCategory category)
        {
            var priceListItem = _items.Single(x =>
                x.CountryCode == countryCode && x.SubscriptionPeriod == subscriptionPeriod &&
                x.Category == category);

            return priceListItem.Value;
        }
    }
```
### Description

Let's introduce the concepts of the strategy pattern, so we can understand how the above example fits this pattern.

* **Client** - The calling code.
* **Context** - An object which maintains a reference to one of the *concrete strategies* and communicates with the *client*.
* **Strategy interface** - An interface or abstract class that the *client* can use to set a concrete strategy at run-time, through the *context*.
* **Concrete strategies** - One or more implementations of the *strategy interface*.

---

If we have a close look at our example of buying a `Subscription`, we can notice the elements of the strategy pattern.

* `BuySubscriptionCommandHandler` is the calling code! Also the handler, indirectly via the `PriceListFactory` sets the current *strategy* of `PriceList`, so their combined interaction represents the **Client**. 
* `PriceList` is the object which maintains a reference to a pricing strategy, so it represents the **Context**. 
* `IPricingStrategy` represents the **Strategy interface**.
* `DiscountedValueFromPriceListPricingStrategy`, `DirectValueFromPriceListPricingStrategy` and `DirectValuePricingStrategy` are the implementations of `IPricingStrategy` so they represent the **Concrete strategies**.

---

 The interaction of the `BuySubscriptionCommandHandler` and `PriceListFactory` is a good example of leveraging multiple design patterns. Check out [Factory Pattern](../Factory-Pattern/) to learn more.

Strategy should not be confused with [Decorator](../Decorator-Pattern/)!!!
*A strategy lets you change the guts of an object, while decorator lets you change the skin.*