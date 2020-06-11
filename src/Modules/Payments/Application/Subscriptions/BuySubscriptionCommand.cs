using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions
{
    public class BuySubscriptionCommand : CommandBase
    {
        public BuySubscriptionCommand(
            Guid payerId, 
            string subscriptionTypeCode, 
            string countryCode)
        {
            PayerId = payerId;
            SubscriptionTypeCode = subscriptionTypeCode;
            CountryCode = countryCode;
        }

        public Guid PayerId { get; }

        public string SubscriptionTypeCode { get; }

        public string CountryCode { get; }
    }
}