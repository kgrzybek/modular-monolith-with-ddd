using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription
{
    public class BuySubscriptionCommand : CommandBase<Guid>
    {
        public BuySubscriptionCommand(
            string subscriptionTypeCode, 
            string countryCode)
        {
            SubscriptionTypeCode = subscriptionTypeCode;
            CountryCode = countryCode;
        }

        public string SubscriptionTypeCode { get; }

        public string CountryCode { get; }
    }
}