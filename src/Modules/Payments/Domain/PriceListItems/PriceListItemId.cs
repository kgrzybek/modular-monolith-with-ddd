using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems
{
    public class PriceListItemId : AggregateId<PriceListItem>
    {
        public PriceListItemId(Guid value)
            : base(value)
        {
        }
    }
}