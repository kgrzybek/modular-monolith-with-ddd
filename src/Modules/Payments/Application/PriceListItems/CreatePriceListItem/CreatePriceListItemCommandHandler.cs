using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.CreatePriceListItem
{
    internal class CreatePriceListItemCommandHandler : ICommandHandler<CreatePriceListItemCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        internal CreatePriceListItemCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public Task<Guid> Handle(CreatePriceListItemCommand command, CancellationToken cancellationToken)
        {
            var priceListItem = PriceListItem.Create(
                command.CountryCode,
                SubscriptionPeriod.Of(command.SubscriptionPeriodCode),
                PriceListItemCategory.Of(command.CategoryCode),
                MoneyValue.Of(command.PriceValue, command.PriceCurrency));

            _aggregateStore.AppendChanges(priceListItem);

            return Task.FromResult(priceListItem.Id);
        }
    }
}