using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.ChangePriceListItemAttributes
{
    internal class ChangePriceListItemAttributesCommandHandler : ICommandHandler<ChangePriceListItemAttributesCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public ChangePriceListItemAttributesCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task Handle(ChangePriceListItemAttributesCommand command, CancellationToken cancellationToken)
        {
            var priceListItem = await _aggregateStore.Load(new PriceListItemId(command.PriceListItemId));

            if (priceListItem == null)
            {
                throw new InvalidCommandException(new List<string> { "Pricelist item for changing must exist." });
            }

            priceListItem.ChangeAttributes(
                command.CountryCode,
                SubscriptionPeriod.Of(command.SubscriptionPeriodCode),
                PriceListItemCategory.Of(command.CategoryCode),
                MoneyValue.Of(command.PriceValue, command.PriceCurrency));

            _aggregateStore.AppendChanges(priceListItem);
        }
    }
}