using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Rules
{
    public class SubscriptionForRenewalMustExist : IBusinessRule
    {
        private readonly Guid _subscriptionId;
        
        public SubscriptionForRenewalMustExist(Guid subscriptionId)
        {
            _subscriptionId = subscriptionId;
        }

        public bool IsBroken() => _subscriptionId == Guid.Empty;

        public string Message => "Subscription for renewal must exist.";
    }
}