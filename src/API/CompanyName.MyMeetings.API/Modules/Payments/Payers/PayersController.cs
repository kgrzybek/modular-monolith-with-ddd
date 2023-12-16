using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetPayerSubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Payments.Payers
{
    [Route("api/payments/payers")]
    [ApiController]
    public class PayersController : ControllerBase
    {
        private readonly IPaymentsModule _paymentsModule;

        public PayersController(IPaymentsModule paymentsModule)
        {
            _paymentsModule = paymentsModule;
        }

        [HttpGet("authenticated/subscription")]
        [HasPermission(PaymentsPermissions.GetAuthenticatedPayerSubscription)]
        [ProducesResponseType(typeof(SubscriptionDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthenticatedPayerSubscription()
        {
            var subscription = await _paymentsModule.ExecuteQueryAsync(new GetAuthenticatedPayerSubscriptionQuery());

            return Ok(subscription);
        }
    }
}