using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Payments
{
    [Route("api/payments/subscriptionRenewals")]
    [ApiController]
    public class SubscriptionRenewalsController : ControllerBase
    {
        private readonly IPaymentsModule _paymentsModule;

        public SubscriptionRenewalsController(IPaymentsModule paymentsModule)
        {
            _paymentsModule = paymentsModule;
        }

        [HttpPost]
        [HasPermission(PaymentsPermissions.RegisterPayment)]
        public async Task<IActionResult> RegisterSubscriptionPayment(RegisterSubscriptionRenewalPaymentRequest request)
        {
            await _paymentsModule.ExecuteCommandAsync(
                new MarkSubscriptionRenewalPaymentAsPaidCommand(request.PaymentId));

            return Ok();
        }
    }
}