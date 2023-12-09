using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.ActivatePriceListItem
{
    public class ActivatePriceListItemCommand : CommandBase
    {
        public ActivatePriceListItemCommand(Guid priceListItemId)
        {
            PriceListItemId = priceListItemId;
        }

        public Guid PriceListItemId { get; }
    }
}