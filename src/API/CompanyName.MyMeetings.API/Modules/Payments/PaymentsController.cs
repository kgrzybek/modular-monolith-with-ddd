using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.RegisterPayment;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Payments
{
    [Route("api/payments/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsModule _meetingsModule;

        public PaymentsController(IPaymentsModule meetingsModule)
        {
            _meetingsModule = meetingsModule;
        }

        [HttpPost("{meetingGroupId}")]
        [HasPermission(PaymentsPermissions.RegisterPayment)]
        public async Task<IActionResult> RegisterPayment(Guid meetingGroupId, RegisterPaymentRequest request)
        {
            await _meetingsModule.ExecuteCommandAsync(
                new RegisterPaymentCommand(meetingGroupId, request.StartDate, request.EndDate));

            return Ok();
        }
    }
}
