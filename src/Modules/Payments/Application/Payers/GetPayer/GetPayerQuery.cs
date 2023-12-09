using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer
{
    public class GetPayerQuery : QueryBase<PayerDto>
    {
        public GetPayerQuery(Guid payerId)
        {
            PayerId = payerId;
        }

        public Guid PayerId { get; }
    }
}