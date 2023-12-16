using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Payments.Subscriptions
{
    [Route("api/payments/subscriptionPayments")]
    [ApiController]
    public class SubscriptionPaymentsController : ControllerBase
    {
        private readonly IPaymentsModule _meetingsModule;

        public SubscriptionPaymentsController(IPaymentsModule meetingsModule)
        {
            _meetingsModule = meetingsModule;
        }

        [HttpPost]
        [HasPermission(PaymentsPermissions.RegisterPayment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterSubscriptionPayment(RegisterSubscriptionPaymentRequest request)
        {
            await _meetingsModule.ExecuteCommandAsync(new MarkSubscriptionPaymentAsPaidCommand(request.PaymentId));

            return Ok();
        }
    }
}