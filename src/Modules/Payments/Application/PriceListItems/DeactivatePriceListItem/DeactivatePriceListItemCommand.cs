using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.DeactivatePriceListItem
{
    public class DeactivatePriceListItemCommand : CommandBase
    {
        public DeactivatePriceListItemCommand(Guid priceListItemId)
        {
            PriceListItemId = priceListItemId;
        }

        public Guid PriceListItemId { get; }
    }
}