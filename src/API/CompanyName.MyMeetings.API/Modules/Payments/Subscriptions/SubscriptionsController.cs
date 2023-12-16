using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscriptionRenewal;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Payments.Subscriptions
{
    [Route("api/payments/subscriptions")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IPaymentsModule _meetingsModule;

        public SubscriptionsController(IPaymentsModule meetingsModule)
        {
            _meetingsModule = meetingsModule;
        }

        [HttpPost("")]
        [HasPermission(PaymentsPermissions.BuySubscription)]
        public async Task<IActionResult> BuySubscription(BuySubscriptionRequest request)
        {
            var paymentId = await _meetingsModule.ExecuteCommandAsync(
                new BuySubscriptionCommand(
                    request.SubscriptionTypeCode,
                    request.CountryCode,
                    request.Value,
                    request.Currency));

            return Ok(paymentId);
        }

        [HttpPost("{subscriptionId}/renewals")]
        [HasPermission(PaymentsPermissions.RenewSubscription)]
        public async Task<IActionResult> RenewSubscription(
            Guid subscriptionId,
            RenewSubscriptionRequest request)
        {
            await _meetingsModule.ExecuteCommandAsync(
                new BuySubscriptionRenewalCommand(
                    subscriptionId,
                    request.SubscriptionTypeCode,
                    request.CountryCode,
                    request.Value,
                    request.Currency));

            return Accepted();
        }
    }
}