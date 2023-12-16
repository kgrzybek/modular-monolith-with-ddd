using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Payers
{
    public class PayerId : TypedIdValueBase
    {
        public PayerId(Guid value)
            : base(value)
        {
        }
    }
}