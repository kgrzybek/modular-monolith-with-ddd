using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.ActivatePriceListItem
{
    internal class ActivatePriceListItemCommandHandler : ICommandHandler<ActivatePriceListItemCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public ActivatePriceListItemCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task Handle(ActivatePriceListItemCommand command, CancellationToken cancellationToken)
        {
            var priceListItem = await _aggregateStore.Load(new PriceListItemId(command.PriceListItemId));

            if (priceListItem == null)
            {
                throw new InvalidCommandException(new List<string> { "Pricelist item for activation must exist." });
            }

            priceListItem.Activate();

            _aggregateStore.AppendChanges(priceListItem);
        }
    }
}